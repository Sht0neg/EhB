using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Servises.Tests
{
    [TestFixture]
    public class ServicesMoq
    {
        Mock<MySqlConnection> _connnection;
        Mock<MySqlCommand> _command;
        Mock<MySqlDataAdapter> _adapter;
        Mock<Initialisation> _services;

        [SetUp]
        public void SetUp() {
            _services = new Mock<Initialisation>();
            _connnection = new Mock<MySqlConnection>(string.Empty);
            _command = new Mock<MySqlCommand>(string.Empty, _connnection.Object);
            _adapter = new Mock<MySqlDataAdapter>(_command.Object);

            _connnection.Setup(s => s.Open());
            _command.Setup(c => c.ExecuteReader()).Returns(new Mock<MySqlDataReader>().Object);
        }
    }
}
