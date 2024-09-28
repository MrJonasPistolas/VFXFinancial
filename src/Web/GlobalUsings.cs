﻿global using MediatR;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.OpenApi.Models;
global using VFXFinancial.Application.Configurations;
global using VFXFinancial.Application.Features.Commands.AddEdit;
global using VFXFinancial.Application.Features.Commands.Delete;
global using VFXFinancial.Application.Interfaces.Serialization.Options;
global using VFXFinancial.Application.Interfaces.Serialization.Serializers;
global using VFXFinancial.Application.Interfaces.Serialization.Settings;
global using VFXFinancial.Application.Serialization.JsonConverters;
global using VFXFinancial.Application.Serialization.Options;
global using VFXFinancial.Application.Serialization.Serializers;
global using VFXFinancial.Application.Serialization.Settings;
global using VFXFinancial.Infrastructure.Contexts;
global using VFXFinancial.Shared.Middlewares;