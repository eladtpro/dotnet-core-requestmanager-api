using System.Collections.Generic;
using System.Threading.Tasks;
using RequestManager.Cosmos;
using RequestManager.Model;
using System.Linq;
using System;

namespace RequestManager.Services
{
	public class RequestService //: CosmosDbService<Request>
	{
		public RequestService(CosmosDb cosmosDb) //: base(cosmosDb)
		{

		}

		public async Task<IEnumerable<Request>> ListAsync(string query)
		{
			return await Task.Run(() =>
			{
				return Dummy.Requests.Values;
			});
		}

		public async Task<Request> AddAsync(Request entity)
		{
			return await Task.Run(() =>
			{
				int id = (Dummy.Requests.Values.ToList().Max(r => r.Id) + 1);
				Request request = new Request
				{
					Id = id,
					Key = Guid.NewGuid(),
					User = "Snow.Short\\EPLODE",
					Email = "ingrid.lawson@genmom.com",
					Package = new Package { Name = "Boone@latest", Type = PackageType.Npm },
					SubmittedOn = DateTime.Parse("2018-04-21T02:21:31"),
					Status = RequestStatus.Pending,
					Distribution = DistributionType.Broadcust
				};

				Dummy.Requests[id] = request;
				return request;
			});
		}

		public async Task<Request> DeleteAsync(int id)
		{
			return await Task.Run(() =>
			{
				Dummy.Requests.TryRemove(id, out Request request);
				return request;
			});
		}

		public async Task<Request> GetAsync(int id)
		{
			return await Task.Run(() =>
			{
				return Dummy.Requests[id];
			});

		}

		public async Task<Request> UpdateAsync(Request entity)
		{

			return await Task.Run(() =>
			{
				Dummy.Requests[entity.Id] = entity;
				return Dummy.Requests[entity.Id];
			});
		}

	}
}
