using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IntegrationTest.Models;
using Microsoft.AspNetCore.Mvc.Testing;
using RESTFulSense.Clients;
using WalletSystemAPI;
using WalletSystemAPI.Models;

namespace IntegrationTest.Brokers
{
	public partial class CurrencyApiBroker
	{
		private const string url = "api/currency";

		public async ValueTask<Currency> PostCurrencyAsync(Currency currency) =>
			await ApiFactoryClient.PostContentAsync(url, currency);

		public async ValueTask<Curr> PostStudentAsync(Student student) =>
			await this.ApiFactoryClient.PostContentAsync(StudentsRelativeUrl, student);

		public async ValueTask<Student> GetStudentByIdAsync(Guid studentId) =>
			await this.ApiFactoryClient.GetContentAsync<Student>($"{StudentsRelativeUrl}/{studentId}");

		public async ValueTask<Student> DeleteStudentByIdAsync(Guid studentId) =>
			await this.ApiFactoryClient.DeleteContentAsync<Student>($"{StudentsRelativeUrl}/{studentId}");
	}
}