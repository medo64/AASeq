FROM mcr.microsoft.com/dotnet/runtime:9.0-alpine

# Setup arguments
ARG UID=28433
ARG UNAME=aaseq

# Use root for setup
USER root

# Environment
ENV ENV="/etc/profile"
RUN echo "alias ll='ls -la'" > /etc/profile.d/00-aliases.sh

# Bash
RUN apk add bash
COPY src/AASeq.Docker/bash-default.sh /etc/bash/default.sh

# Helper packages
RUN apk add bash bind-tools tcpdump

# Examples
COPY examples/Files/* /app/examples/

# Executables
WORKDIR /app/bin
COPY bin/linux-musl-x64/AASeq ./
COPY bin/linux-musl-x64/*.dll ./
COPY bin/linux-musl-x64/*.so  ./
RUN  chmod +x AASeq

# Executables
WORKDIR /app
COPY src/AASeq.Docker/aaseq.sh aaseq
RUN chmod +x aaseq

# Adjust permissions
RUN chown -R $UID:$UID /app

# User
RUN addgroup -g $UID $UNAME
RUN adduser -D -G $UNAME $UNAME

# Done
USER $UNAME
ENTRYPOINT [ "bash" ]
