using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Domain;
using MediatR;
using Persistence;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Activity Activity {get; set;}
        }

        public class Handler : IRequestHandler<Command>
        {
        private readonly DataContext Context;
        private readonly IMapper Mapper;
            public Handler(DataContext context, IMapper mapper)
            {
            Mapper = mapper;
            Context = context;
                
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await Context.Activities.FindAsync(request.Activity.Id);
                Mapper.Map(request.Activity, activity);
                await Context.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}