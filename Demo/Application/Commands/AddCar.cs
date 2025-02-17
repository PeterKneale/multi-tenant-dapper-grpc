﻿namespace Demo.Application.Commands;

public static class AddCar
{
    public class Command : IRequest
    {
        public Command(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; }
    }

    internal class Validator : AbstractValidator<Command>
    {
        public Validator()
        {
            RuleFor(x => x.Id).NotEmpty();
        }
    }

    internal class Handler : IRequestHandler<Command>
    {
        private readonly ICarRepository _cars;

        public Handler(ICarRepository cars)
        {
            _cars = cars;
        }

        public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
        {
            var carId = CarId.CreateInstance(request.Id);

            var car = Car.CreateInstance(carId);

            await _cars.Insert(car, cancellationToken);

            return Unit.Value;
        }
    }
}