﻿using Ardalis.GuardClauses;
using MediatR;
using TheStore.SharedModels.Models;

namespace TheStore.Web.BlazorApp.Client.Mediator.Commands
{
    public class AddItemToCartCommand : IRequest<Result>
    {
        public string Sku { get; }

        public AddItemToCartCommand(string sku)
        {
            Guard.Against.NullOrWhiteSpace(sku, nameof(sku));

            Sku = sku;
        }
    }
}