﻿global using AutoMapper;
global using FluentValidation;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Options;
global using MediatR;
global using Newtonsoft.Json;
global using System.ComponentModel.DataAnnotations;
global using System.Reflection;
global using System.Text.Json;
global using System.Text.RegularExpressions;
global using VFXFinancial.Application.Configurations;
global using VFXFinancial.Application.Interfaces.Repositories;
global using VFXFinancial.Application.Interfaces.Serialization.Options;
global using VFXFinancial.Application.Interfaces.Serialization.Serializers;
global using VFXFinancial.Application.Interfaces.Serialization.Settings;
global using VFXFinancial.Application.Features.Commands.AddEdit;
global using VFXFinancial.Application.Serialization.Options;
global using VFXFinancial.Application.Serialization.Settings;
global using VFXFinancial.Application.Serializers;
global using VFXFinancial.Domain.Contracts;
global using VFXFinancial.Domain.Entities;
global using VFXFinancial.Shared.Wrapper;