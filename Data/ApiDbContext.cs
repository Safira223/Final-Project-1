using Microsoft.EntityFrameworkCore;
using PaymentAPI.Models;
using PaymentAPI.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI;
using MySql.Data;
using MySql.EntityFrameworkCore;

namespace PaymentAPI.Data
{
    public class ApiDbContext : IdentityDbContext
    {
        public virtual DbSet<PaymentData> PaymentDatas {get;set;}

        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
        {

        }

    }
}