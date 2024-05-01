using MediatR;
using Newtonsoft.Json;
using Quartz;

namespace Infrastructure.BackgroundJobs;

[DisallowConcurrentExecution]
internal sealed class ProcessOutboxMessagesJob(ApplicationWriteDbContext dbContext, IPublisher publisher) : IJob
{
    public async Task Execute(IJobExecutionContext context)
    {
        List<OutboxMessage> messages = await dbContext.Set<OutboxMessage>()
            .Where(m => m.ProccessedAtUtc == null)
            .OrderBy(m => m.CreatedAtUtc)
            .Take(20)
            .ToListAsync(context.CancellationToken);

        foreach (OutboxMessage message in messages)
        {
            IDomainEvent? domainEvent = JsonConvert
                .DeserializeObject<IDomainEvent>(message.Content,
                    new JsonSerializerSettings
                    {
                        TypeNameHandling = TypeNameHandling.All
                    });

            if (domainEvent is null)
            {
                continue;
            }

            await publisher.Publish(domainEvent, context.CancellationToken);
            message.ProccessedAtUtc = DateTime.UtcNow;
            await dbContext.SaveChangesAsync();
        }
    }
}
