using Microsoft.AspNetCore.Authorization;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Core.Setup.Swagger
{
    internal class SecurityRequirementsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            var controllerAuthAttributes = context
                    .MethodInfo?
                    .DeclaringType?
                    .GetCustomAttributes(true)
                    .OfType<AuthorizeAttribute>();

            var methodAuthAttributes = context
                    .MethodInfo?
                    .GetCustomAttributes(true)
                    .OfType<AuthorizeAttribute>();

            var isMethodSecured = methodAuthAttributes != null && methodAuthAttributes.Any();
            var isControllerSecured = controllerAuthAttributes != null && controllerAuthAttributes.Any();

            if (isMethodSecured || isControllerSecured)
            {
                if (!operation.Responses.ContainsKey("401"))
                    operation.Responses.Add("401", new OpenApiResponse { Description = "If Authorization header not present, has no value or no valid jwt bearer token" });

                if (!operation.Responses.ContainsKey("403"))
                    operation.Responses.Add("403", new OpenApiResponse { Description = "If user not authorized to perform requested action" });

                var jwtAuthScheme = new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                };

                operation.Security = new List<OpenApiSecurityRequirement>
            {
                new OpenApiSecurityRequirement
                {
                    [ jwtAuthScheme ] = new List<string>()
                }
            };
            }
        }
    }
}