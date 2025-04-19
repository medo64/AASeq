namespace AASeq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Net;
using System.Text;

public sealed partial class AASeqNodes : IParsable<AASeqNodes> {

    /// <summary>
    /// Loads document from a stream.
    /// </summary>
    /// <param name="stream">Stream containing the UTF-8 text.</param>
    public static AASeqNodes Load(Stream stream) {
        return Load(stream, AASeqInputOptions.Permissive);
    }

    /// <summary>
    /// Loads document from a stream.
    /// </summary>
    /// <param name="stream">Stream containing the UTF-8 text.</param>
    /// <param name="options">Parsing options.</param>
    public static AASeqNodes Load(Stream stream, AASeqInputOptions options) {
        ArgumentNullException.ThrowIfNull(stream);

        using var bufferStream = new BufferedStream(stream, BufferSize);
        using var readerStream = new StreamReader(bufferStream, Utf8);
        return LoadDocument(readerStream, options);
    }

    /// <summary>
    /// Loads document from a reader.
    /// </summary>
    /// <param name="reader">Text reader.</param>
    public static AASeqNodes Load(TextReader reader) {
        return Load(reader, AASeqInputOptions.Permissive);
    }

    /// <summary>
    /// Loads document from a reader.
    /// </summary>
    /// <param name="reader">Text reader.</param>
    /// <param name="options">Parsing options.</param>
    public static AASeqNodes Load(TextReader reader, AASeqInputOptions options) {
        ArgumentNullException.ThrowIfNull(reader);

        return LoadDocument(reader, options);
    }

    /// <summary>
    /// Loads document from a file.
    /// </summary>
    /// <param name="filePath">Path to the UTF-8 encoded file.</param>
    public static AASeqNodes Load(string filePath) {
        return Load(filePath, AASeqInputOptions.Permissive);
    }

    /// <summary>
    /// Loads document from a file.
    /// </summary>
    /// <param name="filePath">Path to the UTF-8 encoded file.</param>
    /// <param name="options">Parsing options.</param>
    public static AASeqNodes Load(string filePath, AASeqInputOptions options) {
        ArgumentNullException.ThrowIfNull(filePath);

        using var stream = File.OpenRead(filePath);
        return Load(stream, options);
    }


    #region IParsable<AASeqNodes>

    /// <summary>
    /// Parses a string into a KDL document.
    /// </summary>
    /// <param name="s">Text.</param>
    public static AASeqNodes Parse(string s) {
        return Parse(s, AASeqInputOptions.Permissive);
    }

    /// <summary>
    /// Parses a string into a KDL document.
    /// </summary>
    /// <param name="s">Text.</param>
    /// <param name="provider">Provider; not used.</param>
    public static AASeqNodes Parse(string s, IFormatProvider? provider) {
        return Parse(s, AASeqInputOptions.Permissive);
    }

    /// <summary>
    /// Parses a string into a KDL document.
    /// </summary>
    /// <param name="s">Text.</param>
    /// <param name="options">Parsing options.</param>
    public static AASeqNodes Parse(string s, AASeqInputOptions options) {
        ArgumentNullException.ThrowIfNull(s);

        using var memoryStream = new MemoryStream(Utf8.GetBytes(s));
        using var readerStream = new StreamReader(memoryStream, Utf8);
        return LoadDocument(readerStream, options);
    }

    /// <summary>
    /// Returns true if document can be parsed.
    /// </summary>
    /// <param name="s">Text.</param>
    /// <param name="result">Document.</param>
    public static bool TryParse([NotNullWhen(true)] string? s, [MaybeNullWhen(false)] out AASeqNodes result) {
        return TryParse(s, AASeqInputOptions.Permissive, out result);
    }

    /// <summary>
    /// Returns true if document can be parsed.
    /// </summary>
    /// <param name="s">Text.</param>
    /// <param name="provider">Provider; not used.</param>
    /// <param name="result">Document.</param>
    public static bool TryParse([NotNullWhen(true)] string? s, IFormatProvider? provider, [MaybeNullWhen(false)] out AASeqNodes result) {
        return TryParse(s, AASeqInputOptions.Permissive, out result);
    }

    /// <summary>
    /// Returns true if document can be parsed.
    /// </summary>
    /// <param name="s">Text.</param>
    /// <param name="options">Input options.</param>
    /// <param name="result">Document.</param>
    public static bool TryParse([NotNullWhen(true)] string? s, AASeqInputOptions options, [MaybeNullWhen(false)] out AASeqNodes result) {
        try {
            using var memoryStream = new MemoryStream(Utf8.GetBytes(s ?? string.Empty));
            using var readerStream = new StreamReader(memoryStream, Utf8);
            result = LoadDocument(readerStream, options);
            return true;
        } catch (InvalidOperationException) {  // not really main use case so no specific non-exception parsing
            result = null;
            return false;
        }
    }

    #endregion IParsable<AASeqNodes>


    #region Helper

    private enum State {
        AwaitNode, NodeName,
        AwaitArgument, ValueOrPropertyName,
        PropertyValue, QuotedString, AwaitBlockString, BlockString,
        ValueAnnotation,
        LineComment, BlockComment
    }

    private static AASeqNodes LoadDocument(TextReader reader, AASeqInputOptions options) {
        var sw = Stopwatch.StartNew();
        try {
            var document = new AASeqNodes();

            var hadValue = false;
            var sbNodeName = StringBuilderPool.Get();
            var sbValueAnnotation = StringBuilderPool.Get();
            var sbArgument = StringBuilderPool.Get();
            var sbPropertyValue = StringBuilderPool.Get();
            var sbQuotedString = StringBuilderPool.Get();
            try {
                var blockQuoteCountStart = 0;
                var blockQuoteCountEnd = 0;

                AASeqNode? lastNode = null;
                var nodeTree = new Stack<(AASeqNode? Node, AASeqNodes Nodes)>([(null, document)]);

                (int nLine, int nChar) = (1, 0);
                var skipLf = false;

                var state = new Stack<State>([State.AwaitNode]);
                void SetState(State newState) { state.Pop(); state.Push(newState); }
                void PushState(State subState) { state.Push(subState); }
                void PopState() { state.Pop(); }

                void AddNode() {
                    try {
                        var nodeNameRes = sbNodeName.ToString();
                        var newNode = new AASeqNode(nodeNameRes);
                        nodeTree.Peek().Nodes.Add(newNode);
                        lastNode = newNode;
                        hadValue = false;
                        Debug.WriteLine($"[AASeq.Document] Added node '{newNode.Name}'");
                    } catch (ArgumentException ex) {
                        throw new InvalidOperationException($"Cannot add node at line {nLine}, character {nChar}.", ex);
                    } finally {
                        sbNodeName.Length = 0;
                    }
                }
                void AddValue() {
                    if (lastNode is null) { throw new InvalidOperationException($"Cannot add value at line {nLine}, character {nChar}."); }
                    try {
                        string annotationRes = sbValueAnnotation.ToString().Trim();
                        string valueRes = sbArgument.ToString();
                        if ((sbValueAnnotation.Length > 0) && (annotationRes.Length == 0)) { throw new InvalidOperationException($"Cannot add value at line {nLine}, character {nChar}."); }
                        if (!TryParseValue(valueRes, annotationRes, out var newValue, options)) { throw new InvalidOperationException($"Cannot convert value at line {nLine}, character {nChar}."); }
                        if (hadValue && options.NoDuplicatesValues) {
                            throw new InvalidOperationException($"Cannot duplicate value at line {nLine}, character {nChar}.");
                        } else {
                            lastNode.Value = new AASeqValue(newValue);
                            hadValue = true;
                        }
                        Debug.WriteLine($"[AASeq.Document] Added value '{newValue}'");
                    } catch (ArgumentException ex) {
                        throw new InvalidOperationException($"Cannot convert value at line {nLine}, character {nChar}.", ex);
                    } finally {
                        sbArgument.Length = 0;
                        sbValueAnnotation.Length = 0;
                    }
                }
                void AddProperty() {
                    if (lastNode is null) { throw new InvalidOperationException($"Cannot add property at line {nLine}, character {nChar}."); }
                    try {
                        var propertyNameRes = sbArgument.ToString();
                        var propertyValueRes = sbPropertyValue.ToString();
                        var propertyValue = Dequote(propertyValueRes);
                        if (!options.NoDuplicatesProperties) {  // keep last property set
                            lastNode.Properties[propertyNameRes] = propertyValue;
                        } else if (lastNode.Properties.ContainsKey(propertyNameRes)) {
                            throw new InvalidOperationException($"Cannot add duplicate property '{propertyNameRes}' at line {nLine}, character {nChar}.");
                        } else {
                            lastNode.Properties.Add(propertyNameRes, propertyValue);
                        }
                        Debug.WriteLine($"[AASeq.Document] Added property '{propertyValueRes}'='{propertyValue}'");
                    } finally {
                        sbArgument.Length = 0;
                        sbPropertyValue.Length = 0;
                    }
                }
                void AppendQuotedString(StringBuilder destination) {
                    try {
                        Debug.Assert(sbQuotedString.Length >= 2);  // it has to have at least two quote chars already
                        destination.Append(sbQuotedString);
                    } finally {
                        sbQuotedString.Length = 0;
                    }
                }
                void EnterNode() {
                    if (lastNode is null) { throw new InvalidOperationException($"Cannot start new node at line {nLine}, character {nChar}."); }
                    nodeTree.Push((lastNode, lastNode.Nodes));
                    lastNode = null;
                }
                void ExitNode() {
                    if (nodeTree.Count <= 1) { throw new InvalidOperationException($"Cannot end node at line {nLine}, character {nChar}."); }
                    var poppedNodes = nodeTree.Pop();
                    lastNode = null;
                }

                int currValue, nextValue = reader.Read();

                int ReadNextChar() {
                    currValue = nextValue;
                    nextValue = reader.Read();  // needed for EOL detection
                    nChar++;  // increment column
                    return nextValue;
                }

                do {  // read one character at a time
                    ReadNextChar();

                    var ch = (char)currValue;
                    var chNext = (char)nextValue;
                    var isEOF = (currValue == -1);
                    var isEOL = isEOF || (currValue is '\n' or '\r');

                    var currState = state.Peek();
                    State? forceState = null;

                    // process comments
                    if (currState != State.QuotedString) {
                        if (ch is '#') {
                            isEOL = true;  // simulate EOL when the next char is comment
                            forceState = State.LineComment;
                        } else if ((ch is '/') && (chNext is '/')) {
                            isEOL = true;  // simulate EOL when the next char is comment
                            ReadNextChar();  // consume the next char
                            forceState = State.LineComment;
                        } else if ((ch is '/') && (chNext is '*')) {
                            isEOL = true;  // simulate EOL when the next char is comment
                            ReadNextChar();  // consume the next char
                            forceState = State.BlockComment;
                        }
                    }

                    // normal processing
                    //Debug.WriteLine($"[AASeq.Document] ({currState})'{ch}'");
                    switch (currState) {
                        case State.AwaitNode: {
                                if (isEOL || (ch is ';')) {  // ignore empty nodes
                                } else if (char.IsWhiteSpace(ch)) {  // ignore leading whitespace
                                } else if (ch is '{') {
                                    EnterNode();
                                } else if (ch is '}') {
                                    ExitNode();
                                } else if (ch is '(' or ')' or '\\' or '=' or '"' or '#') {
                                    throw new InvalidOperationException($"Unexpected character at line {nLine}, character {nChar}.");
                                } else {
                                    sbNodeName.Append(ch);
                                    SetState(State.NodeName);
                                }
                            }
                            break;

                        case State.NodeName: {
                                if (isEOL || (ch == ';')) {
                                    AddNode();
                                    SetState(State.AwaitNode);
                                } else if (char.IsWhiteSpace(ch)) {
                                    AddNode();
                                    SetState(State.AwaitArgument);
                                } else if (ch is '{') {
                                    AddNode();
                                    EnterNode();
                                    SetState(State.AwaitNode);
                                } else if (ch is '}') {
                                    AddNode();
                                    ExitNode();
                                    SetState(State.AwaitNode);
                                } else if (ch is '(' or ')' or '\\' or '=' or '"' or '#') {
                                    throw new InvalidOperationException($"Unexpected character at line {nLine}, character {nChar}.");
                                } else {  // just continue with name
                                    sbNodeName.Append(ch);
                                }
                            }
                            break;

                        case State.AwaitArgument: {
                                if (isEOL || (ch == ';')) {
                                    if (sbValueAnnotation.Length > 0) { throw new InvalidOperationException($"Cannot add value at line {nLine}, character {nChar}."); }
                                    SetState(State.AwaitNode);
                                } else if (char.IsWhiteSpace(ch)) {  // ignore leading whitespace
                                } else if (ch is '{') {
                                    EnterNode();
                                    SetState(State.AwaitNode);
                                } else if (ch is '}') {
                                    ExitNode();
                                    SetState(State.AwaitNode);
                                } else if (ch is '(') {
                                    SetState(State.ValueAnnotation);
                                } else if (ch is '"') {
                                    sbQuotedString.Append(ch);  // include quotes in quoted string (will be sorted out later)
                                    SetState(State.ValueOrPropertyName);
                                    PushState(State.QuotedString);
                                } else if (ch is ')' or '\\' or '=' or '#') {
                                    throw new InvalidOperationException($"Unexpected character at line {nLine}, character {nChar}.");
                                } else {
                                    sbArgument.Append(ch);
                                    SetState(State.ValueOrPropertyName);
                                }
                            }
                            break;

                        case State.ValueOrPropertyName: {
                                if (isEOL || (ch == ';')) {
                                    AddValue();
                                    SetState(State.AwaitNode);
                                } else if (char.IsWhiteSpace(ch)) {
                                    AddValue();
                                    SetState(State.AwaitArgument);
                                } else if (ch is '{') {
                                    AddValue();
                                    EnterNode();
                                    SetState(State.AwaitNode);
                                } else if (ch is '}') {
                                    AddValue();
                                    ExitNode();
                                    SetState(State.AwaitNode);
                                } else if (ch is '=') {
                                    if (chNext == '"') {
                                        sbQuotedString.Append(chNext);
                                        ReadNextChar();  // just consume the next char
                                        SetState(State.PropertyValue);
                                        PushState(State.QuotedString);
                                    } else {
                                        SetState(State.PropertyValue);
                                    }
                                } else if (ch is '(' or ')' or '\\' or '"' or '#') {
                                    throw new InvalidOperationException($"Unexpected character at line {nLine}, character {nChar}.");
                                } else {  // just continue value
                                    sbArgument.Append(ch);
                                }
                            }
                            break;

                        case State.PropertyValue: {
                                if (isEOL || (ch == ';')) {
                                    AddProperty();
                                    SetState(State.AwaitNode);
                                } else if (char.IsWhiteSpace(ch)) {
                                    AddProperty();
                                    SetState(State.AwaitArgument);
                                } else if (ch is '{') {
                                    AddProperty();
                                    EnterNode();
                                    SetState(State.AwaitNode);
                                } else if (ch is '}') {
                                    AddProperty();
                                    ExitNode();
                                    SetState(State.AwaitNode);
                                } else if (ch is '(' or ')' or '\\' or '"' or '#') {
                                    throw new InvalidOperationException($"Unexpected character at line {nLine}, character {nChar}.");
                                } else {  // just continue with value
                                    sbPropertyValue.Append(ch);
                                }
                            }
                            break;

                        case State.ValueAnnotation: {
                                if (isEOL || (ch == ';')) {
                                    throw new InvalidOperationException($"Cannot end annotation at line {nLine}, character {nChar}.");
                                } else if (ch is ')') {
                                    SetState(State.AwaitArgument);
                                } else if (ch is '{' or '}' or '(' or '\\' or '"' or '#') {
                                    throw new InvalidOperationException($"Unexpected character at line {nLine}, character {nChar}.");
                                } else {
                                    sbValueAnnotation.Append(ch);
                                }
                            }
                            break;

                        case State.QuotedString: {
                                if (ch is '"') {
                                    PopState();
                                    if ((sbQuotedString.Length == 1) && (chNext is '"')) {
                                        sbQuotedString.Append(ch);
                                        blockQuoteCountStart = 2;
                                        PushState(State.AwaitBlockString);
                                    } else {  // done with quoted string
                                        var oldState = state.Peek();
                                        if (oldState == State.ValueOrPropertyName) {
                                            sbQuotedString.Append(ch);
                                            AppendQuotedString(sbArgument);
                                            AddValue();
                                            SetState(State.AwaitArgument);
                                        } else if (oldState == State.PropertyValue) {
                                            sbQuotedString.Append(ch);
                                            AppendQuotedString(sbPropertyValue);
                                            AddProperty();
                                            SetState(State.AwaitArgument);
                                        } else {
                                            throw new InvalidOperationException($"Cannot end quoted string at {nLine}, character {nChar}.");
                                        }
                                    }
                                } else if (ch is '\\') {
                                    sbQuotedString.Append(ch);
                                    if (chNext is '"' or '\\' or '0' or 'a' or 'b' or 'e' or 'f' or 'n' or 'r' or 't' or 'v') {
                                        sbQuotedString.Append(chNext);
                                        ReadNextChar();    // to consume the next char
                                    } else if (chNext is 'u' or 'U' or 'x') {
                                        sbQuotedString.Append(chNext);
                                        var hexCount = (chNext is 'u') ? 4 : (chNext is 'U') ? 8 : 2;
                                        for (var i = 0; i < hexCount; i++) {
                                            var chHex = (char)ReadNextChar();
                                            if (chHex is (>= '0' and <= '9') or (>= 'A' and <= 'F') or (>= 'a' and <= 'f')) {
                                                sbQuotedString.Append(chHex);
                                            } else {
                                                throw new InvalidOperationException($"Invalid unicode escape sequence at {nLine}, character {nChar}.");
                                            }
                                        }
                                        ReadNextChar();  // to consume the next char
                                    } else {
                                        throw new InvalidOperationException($"Unrecognized escape sequence at {nLine}, character {nChar}.");
                                    }
                                } else {
                                    sbQuotedString.Append(ch);
                                }
                            }
                            break;

                        case State.AwaitBlockString: {
                                sbQuotedString.Append(ch);
                                if (ch is '"') {
                                    blockQuoteCountStart++;
                                } else {
                                    blockQuoteCountEnd = 0;
                                    SetState(State.BlockString);
                                }
                            }
                            break;

                        case State.BlockString: {
                                sbQuotedString.Append(ch);
                                if (ch is '"') {
                                    blockQuoteCountEnd++;
                                    if (blockQuoteCountEnd == blockQuoteCountStart) {
                                        PopState();
                                        var oldState = state.Peek();
                                        if (oldState == State.ValueOrPropertyName) {
                                            sbQuotedString.Append(ch);
                                            AppendQuotedString(sbArgument);
                                            AddValue();
                                            SetState(State.AwaitNode);
                                        } else {
                                            throw new InvalidOperationException($"Cannot end quoted string at {nLine}, character {nChar}.");
                                        }
                                    }
                                } else {
                                    blockQuoteCountEnd = 0;
                                }
                            }
                            break;

                        case State.LineComment: {
                                if (isEOL) { PopState(); }
                            }
                            break;

                        case State.BlockComment: {
                                if ((ch is '*') && (chNext is '/')) {
                                    ReadNextChar();
                                    PopState();
                                }
                            }
                            break;

                        default:
                            throw new InvalidOperationException($"Unknown state at line {nLine}, character {nChar}.");
                    }

                    // force new state (e.g. in case of comments)
                    if (forceState is not null) {
                        PushState(forceState.Value);
                    }

                    // figure out unfinished parsing
                    if (isEOF) {
                        if (sbNodeName.Length > 0) { throw new InvalidOperationException("Unfinished node name."); }
                        if (sbValueAnnotation.Length > 0) { throw new InvalidOperationException("Unfinished value annotation."); }
                        if (sbArgument.Length > 0) { throw new InvalidOperationException("Unfinished argument."); }
                        if (sbPropertyValue.Length > 0) { throw new InvalidOperationException("Unfinished property value."); }
                        if (sbQuotedString.Length > 0) { throw new InvalidOperationException("Unfinished quoted string."); }
                    }

                    // figure out EOLs for exceptions
                    if ((currValue == 0x0A) && !skipLf) {  // LF or CRLF
                        nLine++;
                        nChar = 0;
                    } else if (ch == 0x0D) {  // CR or first char of CRLF
                        if (nextValue == 0x0A) { skipLf = true; }
                        nLine++;
                        nChar = 0;
                    }
                } while (currValue >= 0);

                return document;

            } finally {
                sbNodeName.Length = 0;
                StringBuilderPool.Return(sbNodeName);

                sbValueAnnotation.Length = 0;
                StringBuilderPool.Return(sbValueAnnotation);

                sbArgument.Length = 0;
                StringBuilderPool.Return(sbArgument);

                sbPropertyValue.Length = 0;
                StringBuilderPool.Return(sbPropertyValue);

                sbQuotedString.Length = 0;
                StringBuilderPool.Return(sbQuotedString);
            }
        } finally {
            Debug.WriteLine($"[AASeq.Document] Load: {sw.ElapsedMilliseconds} ms");
            Metrics.NodesLoadMilliseconds.Record(sw.ElapsedMilliseconds);
            Metrics.NodesLoadCount.Add(1);
        }
    }

    private static string Dequote(string text) {
        if (text.Length == 0) { return string.Empty; }

        if (text.StartsWith(@"""""""", StringComparison.Ordinal) && text.EndsWith(@"""""""", StringComparison.Ordinal)) {  // block quote
            var quoteCount = 0;
            {  // check quote count
                foreach (var ch in text) {
                    if (ch is '"') {
                        quoteCount++;
                    } else {
                        break;
                    }
                }
            }

            int? minWhitespaceCount = null;
            {  // first count
                int? whitespaceCount = null;  // don't count the first line
                int nLine = 1;
                var skipLf = false;
                foreach (var ch in text[quoteCount..^(quoteCount + 1)]) {
                    if (whitespaceCount is not null) {
                        if (char.IsWhiteSpace(ch)) {
                            whitespaceCount++;
                        } else {
                            if ((minWhitespaceCount == null) || (whitespaceCount < minWhitespaceCount)) {
                                minWhitespaceCount = whitespaceCount;
                            }
                            whitespaceCount = null;  // stop counting
                        }
                    }
                    if (ch == 0x0D) {  // CR or first char of CRLF
                        skipLf = true;
                        nLine++;
                        whitespaceCount = 0;
                    } else if (ch == 0x0A) {  // LF or CRLF
                        if (skipLf) {
                            skipLf = false;
                        } else {
                            nLine++;
                            whitespaceCount = 0;
                        }
                    }
                }
            }

            var sbOutput = StringBuilderPool.Get();
            try {  // second round to remove whitespace
                int? whitespaceCount = null;  // don't count the first line
                int nLine = 1;
                var skipLf = false;
                foreach (var ch in text[quoteCount..^(quoteCount + 1)]) {
                    if (whitespaceCount is not null) {
                        if (char.IsWhiteSpace(ch)) {
                            whitespaceCount++;
                            if (whitespaceCount > minWhitespaceCount) { whitespaceCount = null; }
                        } else {
                            whitespaceCount = null;
                        }
                    }
                    if (whitespaceCount is null) { sbOutput.Append(ch); }
                    if (ch == 0x0D) {  // CR or first char of CRLF
                        skipLf = true;
                        nLine++;
                        whitespaceCount = 0;
                    } else if (ch == 0x0A) {  // LF or CRLF
                        if (skipLf) {
                            skipLf = false;
                        } else {
                            nLine++;
                            whitespaceCount = 0;
                        }
                    }
                }
                return DequoteMinor(sbOutput);
            } finally {
                sbOutput.Length = 0;
                StringBuilderPool.Return(sbOutput);
            }
        } else if (text.StartsWith('"') && text.EndsWith('"')) {  // line quote
            var sbOutput = StringBuilderPool.Get();
            try {
                sbOutput.Append(text[1..^1]);  // remove quotes
                return DequoteMinor(sbOutput);
            } finally {
                StringBuilderPool.Return(sbOutput);
            }
        } else {
            return text;
        }
    }

    private static string DequoteMinor(StringBuilder output) {
        for (var i = 0; i < output.Length; i++) {
            var ch = output[i];
            if (ch is '\\') {
                var chNext = output[i + 1];
                if (chNext is '"' or '\\') {
                    output.Remove(i, 1);
                } else if (chNext is '0') {
                    output.Remove(i, 1);
                    output[i] = '\0';
                } else if (chNext is 'a') {
                    output.Remove(i, 1);
                    output[i] = '\a';
                } else if (chNext is 'b') {
                    output.Remove(i, 1);
                    output[i] = '\b';
                } else if (chNext is 'e') {
                    output.Remove(i, 1);
                    output[i] = '\e';
                } else if (chNext is 'f') {
                    output.Remove(i, 1);
                    output[i] = '\f';
                } else if (chNext is 'n') {
                    output.Remove(i, 1);
                    output[i] = '\n';
                } else if (chNext is 'r') {
                    output.Remove(i, 1);
                    output[i] = '\r';
                } else if (chNext is 't') {
                    output.Remove(i, 1);
                    output[i] = '\t';
                } else if (chNext is 'v') {
                    output.Remove(i, 1);
                    output[i] = '\v';
                } else if (chNext is 'x') {
                    var hex = new string([output[i + 2], output[i + 3]]);
                    output[i] = (char)byte.Parse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    output.Remove(i + 1, 3);
                } else if (chNext is 'u') {
                    var hex = new string([output[i + 2], output[i + 3], output[i + 4], output[i + 5]]);
                    output[i] = (char)int.Parse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    output.Remove(i + 1, 5);
                } else if (chNext is 'U') {
                    var hex = new string([output[i + 2], output[i + 3], output[i + 4], output[i + 5], output[i + 6], output[i + 7], output[i + 8], output[i + 9]]);
                    output[i] = (char)long.Parse(hex, NumberStyles.HexNumber, CultureInfo.InvariantCulture);
                    output.Remove(i + 1, 9);
                }
            }
        }
        return output.ToString();
    }

    private static bool TryParseValue(string valueText, string typeAnnotationText, out object? value, AASeqInputOptions options) {
        var typeAnnotation = Dequote(typeAnnotationText);
        if (valueText is null) { value = null; return false; }

        if (string.IsNullOrEmpty(typeAnnotation)) {
            if (valueText.Equals("null", StringComparison.OrdinalIgnoreCase)) {
                value = null; return true;
            } else if (valueText.Equals("false", StringComparison.OrdinalIgnoreCase)) {
                value = false; return true;
            } else if (valueText.Equals("true", StringComparison.OrdinalIgnoreCase)) {
                value = true; return true;
            } else if (valueText.Equals("nan", StringComparison.OrdinalIgnoreCase)) {
                value = double.NaN; return true;
            } else if (valueText.Equals("+inf", StringComparison.OrdinalIgnoreCase)) {
                value = double.PositiveInfinity; return true;
            } else if (valueText.Equals("-inf", StringComparison.OrdinalIgnoreCase)) {
                value = double.NegativeInfinity; return true;
            } else if (((valueText.Length >= 1) && (valueText[0] is >= '0' and <= '9'))
                || ((valueText.Length >= 2) && (valueText[0] is '+' or '-' or '.') && (valueText[1] is >= '0' and <= '9'))
                || ((valueText.Length >= 3) && (valueText[0] is '+' or '-' or '.') && (valueText[1] is >= '.') && (valueText[2] is >= '0' and <= '9'))) {
                if (valueText[0] is '-') {  // negative numbers
                    if (Int32.TryParse(valueText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value32)) {
                        value = value32; return true;
                    } else if (Int64.TryParse(valueText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value64)) {
                        value = value64; return true;
                    } else if (Int128.TryParse(valueText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value128)) {
                        value = value128; return true;
                    } else if (Double.TryParse(valueText, NumberStyles.Float, CultureInfo.InvariantCulture, out var valueDouble)) {
                        value = valueDouble; return true;
                    } else if (options.StrictNumberQuoting) {  // cannot parse
                        value = null; return false;
                    } else {  // parse permissively
                        value = valueText; return true;  // not strictly correct, but return as string even if it looks as a number
                    }
                } else if ((valueText[0] is '0') && (valueText.Length > 1) && (valueText[1] is 'b' or 'B')) {  // binary
                    var valueBin = valueText[2..].Replace("_", "", StringComparison.Ordinal);
                    if (UInt32.TryParse(valueBin, NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var value32)) {
                        value = value32 > (UInt32)int.MaxValue ? value32 : (Int32)value32; return true;
                    } else if (UInt64.TryParse(valueBin, NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var value64)) {
                        value = value64 > (UInt64)long.MaxValue ? value64 : (Int64)value64; return true;
                    } else if (UInt128.TryParse(valueBin, NumberStyles.BinaryNumber, CultureInfo.InvariantCulture, out var value128)) {
                        value = value128 > (UInt128)Int128.MaxValue ? value128 : (Int128)value128; return true;
                    } else {  // cannot parse
                        value = null; return false;
                    }
                } else if ((valueText[0] is '0') && (valueText.Length > 1) && (valueText[1] is 'x' or 'X')) {  // hex
                    var valueHex = valueText[2..].Replace("_", "", StringComparison.Ordinal);
                    if (UInt32.TryParse(valueHex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var value32)) {
                        value = value32 > (UInt32)int.MaxValue ? value32 : (Int32)value32; return true;
                    } else if (UInt64.TryParse(valueHex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var value64)) {
                        value = value64 > (ulong)long.MaxValue ? value64 : (long)value64; return true;
                    } else if (UInt128.TryParse(valueHex, NumberStyles.HexNumber, CultureInfo.InvariantCulture, out var value128)) {
                        value = value128 > (UInt128)Int128.MaxValue ? value128 : (Int128)value128; return true;
                    } else {  // cannot parse
                        value = null; return false;
                    }
                } else {  // positive numbers
                    if (UInt32.TryParse(valueText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value32)) {
                        value = value32 > (UInt32)Int32.MaxValue ? value32 : (Int32)value32; return true;
                    } else if (UInt64.TryParse(valueText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value64)) {
                        value = value64 > (UInt64)Int64.MaxValue ? value64 : (Int64)value64; return true;
                    } else if (UInt128.TryParse(valueText, NumberStyles.Integer, CultureInfo.InvariantCulture, out var value128)) {
                        value = value128 > (UInt128)Int128.MaxValue ? value128 : (Int128)value128; return true;
                    } else if (Double.TryParse(valueText, NumberStyles.Float, CultureInfo.InvariantCulture, out var valueDouble)) {
                        value = valueDouble; return true;
                    } else if (options.StrictNumberQuoting) {  // cannot parse
                        value = null; return false;
                    } else {  // parse permissively
                        value = valueText; return true;  // not strictly correct, but return as string even if it looks as a number
                    }
                }
            }

            value = Dequote(valueText); return true;
        } else {
            if (typeAnnotation.Equals("bool", StringComparison.OrdinalIgnoreCase)) {
                 return AASeqValue.TryParseBoolean(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("i8", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("int8", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("sbyte", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseSByte(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("u8", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("uint8", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("byte", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseByte(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("i16", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("int16", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("short", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseInt16(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("u16", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("uint16", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("ushort", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseUInt16(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("i32", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("int32", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("int", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseInt32(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("u32", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("uint32", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("uint", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseUInt32(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("i64", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("int64", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("long", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseInt64(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("u64", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("uint64", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("ulong", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseUInt64(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("i128", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("int128", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseInt128(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("u128", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("uint128", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseUInt128(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("f16", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("float16", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("half", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseHalf(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("f32", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("float32", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("single", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseSingle(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("f64", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("float64", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("double", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseDouble(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("d128", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("decimal128", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("decimal", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseDecimal(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("datetime", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("dt", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("datetimeoffset", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseDateTimeOffset(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("dateonly", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("date", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseDateOnly(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("timeonly", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("time", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseTimeOnly(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("duration", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("timespan", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseTimeSpan(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("ip", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("ipaddress", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseIPAddress(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("ipv4", StringComparison.OrdinalIgnoreCase)) {
                if (AASeqValue.TryParseIPAddress(Dequote(valueText), out value)) {
                    if (((IPAddress)value!).AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) { return true; }
                }
                value = null; return false;
            } else if (typeAnnotation.Equals("ipv6", StringComparison.OrdinalIgnoreCase)) {
                if (AASeqValue.TryParseIPAddress(Dequote(valueText), out value)) {
                    if (((IPAddress)value!).AddressFamily == System.Net.Sockets.AddressFamily.InterNetworkV6) { return true; }
                }
                value = null; return false;
            } else if (typeAnnotation.Equals("uri", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("url", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseUri(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("uuid", StringComparison.OrdinalIgnoreCase) || typeAnnotation.Equals("guid", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseGuid(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("base64", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseBase64ByteArray(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("hex", StringComparison.OrdinalIgnoreCase)) {
                return AASeqValue.TryParseByteArray(Dequote(valueText), out value);
            } else if (typeAnnotation.Equals("string", StringComparison.OrdinalIgnoreCase)) {
                value = Dequote(valueText);
                return true;
            } else {
                throw new ArgumentOutOfRangeException(nameof(typeAnnotationText), "Unknown type annotation.");
            }
        }
    }

    #endregion Helper

}
