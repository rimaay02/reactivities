using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Delete
    {
        public class Command : IRequest
        {
            public Guid Id {get; set;}
        }
        public class Handler : IRequestHandler<Command>
        {
        private readonly DataContext Context;
            public Handler(DataContext context)
            {
            Context = context;
                
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await Context.Activities.FindAsync(request.Id);
                Context.Remove(activity);
                await Context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}