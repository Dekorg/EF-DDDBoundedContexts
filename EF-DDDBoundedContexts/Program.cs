using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF_DDDBoundedContexts
{
    class Program
    {
        static void Main(string[] args)
        {
            CompanyContext _companyContext = new CompanyContext();
            _companyContext.Cliente.Add(new Cliente { Nombre = "Boris" });
            _companyContext.SaveChanges();

            EnviosContext enviosContext = new EnviosContext();
            var clientes=enviosContext.Clientes.ToList();
            clientes.ForEach(c => Console.WriteLine(c.Nombre));

            enviosContext.Clientes.Add(new Cliente { Nombre = "Pedro" });
            enviosContext.SaveChanges();

            var clientesCompany = _companyContext.Cliente.ToList();
            clientesCompany.ForEach(c => Console.WriteLine(c.Nombre));

            RecurosHumanosContext recurosHumanosContext = new RecurosHumanosContext();
            recurosHumanosContext.Empleados.Add(new Empleado { Nombre = "Nicolás", Sueldo = "15000000" });
            recurosHumanosContext.SaveChanges();

            var empleadosCompany = _companyContext.Empleados.ToList();
            empleadosCompany.ForEach(c => Console.WriteLine(c.Nombre));

            Console.ReadKey();
        }


    }
    public class CompanyContext : DbContext
    {
        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public CompanyContext() : base("CompanyContext") {
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CompanyContext>());
        }
    }

    public class BaseContext<T> : DbContext where T : DbContext
    {
        static BaseContext()
        {
            Database.SetInitializer<T>(null);
        }
        protected BaseContext() : base("DPSalesDatabase")
        {
        }
    }

    public class EnviosContext : DbContext
    {
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Orden> Ordenes { get; set; }
        public EnviosContext() : base("CompanyContext")
        {
            Database.SetInitializer<EnviosContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }

    public class RecurosHumanosContext : DbContext
    {
        public DbSet<Empleado> Empleados { get; set; }
        public RecurosHumanosContext() : base("CompanyContext")
        {
            Database.SetInitializer<EnviosContext>(null);
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }
    }

    public class Orden
    {
        public int OrdenId { get; set; }
        public string Numero { get; set; }
    }

    public class Empleado
    {
        public int EmpleadoId { get; set; }
        public string Nombre { get; set; }
        public string Sueldo { get; set; }
    }
    [Table("Employee")]
    public class Funcionario
    {
        [Key]
        public int EmpleadoId { get; set; }
        public string Nombre { get; set; }
    }
    public class Cliente
    {
        public int ClienteId { get; set; }
        public string Nombre { get; set; }
    }
}
