namespace AASeqPlugin;
using System.Net;
using System.Threading;

internal interface IDiameterThread {

    public void Start(CancellationToken cancellationToken);
    public void Stop();

}
