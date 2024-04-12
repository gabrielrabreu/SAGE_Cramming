﻿using Cramming.UseCases.Topics;
using Cramming.UseCases.Topics.Get;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;

namespace Cramming.API.Topics
{
    public class GetTopicById : EndpointBase
    {
        public override void Configure(WebApplication app)
        {
            app.MapGet(GetTopicByIdRequest.Route, HandleAsync)
                .WithOpenApi()
                .WithName(nameof(GetTopicById))
                .WithTags("Topics")
                .WithSummary("Retrieve topic by ID");
        }

        private async Task<Results<Ok<TopicDTO>, NotFound>> HandleAsync(
            Guid topicId,
            IMediator mediator,
            CancellationToken cancellationToken)
        {
            var query = new GetTopicQuery(topicId);

            var result = await mediator.Send(query, cancellationToken);

            if (result.Status == HttpStatusCode.NotFound)
                return TypedResults.NotFound();

            return TypedResults.Ok(result.Value);
        }
    }
}