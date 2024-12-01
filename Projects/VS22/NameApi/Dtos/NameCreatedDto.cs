using AutoMapper;
using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace NameApi.Dtos
{
    public class NameCreatedDto
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
