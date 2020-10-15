using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfMailSender.Data
{
    class MailSenderDBInitializer : IDesignTimeDbContextFactory<MailSenderDB>
    {
        public MailSenderDB CreateDbContext(string[] args)
        {
            const string connection_string = "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=MailSender.DB;Integrated Security=True";

            var optionsBuilder = new DbContextOptionsBuilder<MailSenderDB>();
            optionsBuilder.UseSqlServer(connection_string);

            return new MailSenderDB(optionsBuilder.Options);
        }
    }
}
