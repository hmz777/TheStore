﻿using Ardalis.GuardClauses;
using TheStore.SharedKernel.ValueObjects;

namespace TheStore.Events.Categories.DomainEvents
{
    public class CategoryUpdatedDomainEvent : IDomainEvent
    {
        public DateTimeOffset DateOccurred { get; private set; }
        public EventStatus Status { get; private set; }
        public MultilanguageString Name { get; private set; }

        public CategoryUpdatedDomainEvent(DateTimeOffset dateOccurred, EventStatus status, MultilanguageString name)
        {
            Guard.Against.Default(dateOccurred, nameof(dateOccurred));
            Guard.Against.Null(name, nameof(name));
            Guard.Against.EnumOutOfRange(status, nameof(status));

            DateOccurred = dateOccurred;
            Status = status;
            Name = name;
        }
    }
}