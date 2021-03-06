﻿using Application.Errors;
using Domain;
using FluentValidation;
using MediatR;
using Pesistence;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Activities
{
    public class Edit
    {
        public class Command : IRequest
        {
            public Guid Id { get; set; }
            public string Title { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public DateTime? Date { get; set; }
            public string City { get; set; }
            public string Venue { get; set; }
        }

        public class CommandValidation : AbstractValidator<Command>
        {
            public CommandValidation()
            {
                RuleFor(x => x.Title).NotEmpty();
                RuleFor(x => x.Description).NotEmpty();
                RuleFor(x => x.Category).NotEmpty();
                RuleFor(x => x.Date).NotEmpty();
                RuleFor(x => x.City).NotEmpty();
                RuleFor(x => x.Venue).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly DataContext _context;

            public Handler(DataContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var activity = await _context.Activities.FindAsync(request.Id);

                if (activity == null)
                    throw new RestException(System.Net.HttpStatusCode.NotFound, new { activity = "Not found" });



                SetEntity(activity, request);

                var success = await _context.SaveChangesAsync() > 0;

                if (success) return Unit.Value;

                throw new Exception("Problem saving changes");
            }

            private Activity SetEntity(Activity entity, Command dto)
            {
                entity.Title = dto.Title ?? entity.Title;
                entity.Description = dto.Description ?? entity.Description;
                entity.Category = dto.Category ?? entity.Category;
                entity.Date = dto.Date ?? entity.Date;
                entity.City = dto.City ?? entity.City;
                entity.Venue = dto.Venue ?? entity.Venue;

                return entity;
            }
        }
    }
}
