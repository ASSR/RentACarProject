using Core.Entities;
using Microsoft.AspNetCore.Http;
using System;

namespace Entities.DTOs
{
    public class CarImageAddDto:IDTO
    {
        public int CarId { get; set; }     
    }
}
