using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Remoting.V2.FabricTransport.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Student.API;
using System.Collections.Generic;
using System.Fabric;
using System.Threading;
using System.Threading.Tasks;

namespace StudentService
{
  /// <summary>
  /// An instance of this class is created for each service instance by the Service Fabric runtime.
  /// </summary>
  internal sealed class StudentService : StatelessService
  {
    private readonly IStudentService studentService;
    public StudentService(StatelessServiceContext context)
        : base(context)
    {
      var studentRepository = new StudentRepository();
      this.studentService = new StudentServiceProvider(studentRepository);
    }

    /// <summary>
    /// Optional override to create listeners (e.g., TCP, HTTP) for this service replica to handle client or user requests.
    /// </summary>
    /// <returns>A collection of listeners.</returns>
    protected override IEnumerable<ServiceInstanceListener> CreateServiceInstanceListeners()
    {
      return new ServiceInstanceListener[]
      {
        new ServiceInstanceListener(
          context => new FabricTransportServiceRemotingListener(
            context,
            studentService,
            new FabricTransportRemotingListenerSettings()
            {
              EndpointResourceName = "StudentService_V1"
            }),
          "StudentService_V1")
      };
    }

    /// <summary>
    /// This is the main entry point for your service instance.
    /// </summary>
    /// <param name="cancellationToken">Canceled when Service Fabric needs to shut down this service instance.</param>
    protected override async Task RunAsync(CancellationToken cancellationToken)
    {
      // TODO: Replace the following sample code with your own logic 
      //       or remove this RunAsync override if it's not needed in your service.

      await base.RunAsync(cancellationToken);
    }
  }
}
