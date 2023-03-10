using Microsoft.AspNetCore.Mvc;
using Orders.Actions.RequestHelpActions.RequestHelpGetMap;

namespace Orders.Actions.RequestHelpActions.RequestHelpGetMap
{
    public class RequestHelpGetMapAction
    {
        private readonly RequestHelpGetMapService _requestHelpGetMapService;

        public RequestHelpGetMapAction(RequestHelpGetMapService requestHelpGetMapService)
        {
            _requestHelpGetMapService = requestHelpGetMapService;
        }

        public async Task<OkObjectResult> Execute()
        {
            var result = await _requestHelpGetMapService.Execute();

            return new OkObjectResult(result);
        }
    }
}