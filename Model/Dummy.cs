using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace RequestManager.Model
{
	public static class Dummy
	{
		private static readonly Lazy<ConcurrentDictionary<string, Request>> requests = new Lazy<ConcurrentDictionary<string, Request>>(
			() => new ConcurrentDictionary<string, Request>(GetRequests()),
			LazyThreadSafetyMode.PublicationOnly);

		public static ConcurrentDictionary<string, Request> Requests => requests.Value;


		private static Dictionary<string, Request> GetRequests()
		{
			int id = 1;

			List<Request> requestData = new List<Request>{
				new Request
				{
					Id = (++id).ToString(),
					Key = new Guid("806a59cd-6356-411e-8208-fbd25f02673c"),
					User = "Snow.Short\\EPLODE",
					Email = "ingrid.lawson@genmom.com",
					Package = new Package { Name = "Boone@latest", Type = PackageType.Npm },
					SubmittedOn = DateTime.Parse("2018-04-21T02:21:31"),
					Status = RequestStatus.Pending,
					Distribution = DistributionType.Broadcust
				},
				new Request
				{
					Id = (++id).ToString(),
					Key = new Guid("ae6c179d-854b-4d12-9dc6-20e8379fd96b"),
					User = "Maynard.Le\\CONJURICA",
					Email = "cara.gates@ersum.com",
					Package = new Package { Name = "Mooney@latest", Type = PackageType.Npm },
					SubmittedOn = DateTime.Parse("2018-08-22T04:33:15"),
					Status = RequestStatus.Pending,
					Distribution = DistributionType.Broadcust
				},
				new Request {
					Id = (++id).ToString(),
					Key = new Guid("e6ae464b-f324-408e-8606-f2be6f65f551"),
					User = "Adkins.Britt\\UNIWORLD",
					Email = "huff.mendoza@surelogic.com",
					Package = new Package { Name = "Ramsey@latest", Type = PackageType.Nuget },
					SubmittedOn = DateTime.Parse("2018-01-19T23:00:16"),
					Status = RequestStatus.Pending,
					Distribution = DistributionType.Broadcust
				},
				new Request {
					Id = (++id).ToString(),
					Key = new Guid("099796ea-160b-4a23-a3a4-d3753a439eb8"),
					User = "Cristina.Justice\\MAROPTIC",
					Email = "latoya.gonzalez@thredz.com",
					Package = new Package { Name = "Mercado@latest", Type = PackageType.Nuget },
					SubmittedOn = DateTime.Parse("2018-03-20T23:55:46"),
					Status = RequestStatus.Pending,
					Distribution = DistributionType.Silent
				},
				new Request {
					Id = (++id).ToString(),
					Key = new Guid("a0d59275-c3b8-4f6c-9228-82231e6eb5c8"),
					User = "Yates.Houston\\ISOTRONIC",
					Email = "carlson.perry@centree.com",
					Package = new Package { Name = "Figueroa@latest", Type = PackageType.Npm },
					SubmittedOn = DateTime.Parse("2018-03-10T21:19:35"),
					Status = RequestStatus.Pending,
					Distribution = DistributionType.Broadcust
				},
				new Request {
					Id = (++id).ToString(),
					Key = new Guid("9f75a9ae-f6b0-4c6d-8333-9d454be06df2"),
					User = "Rhodes.Bishop\\NEWCUBE",
					Email = "evelyn.stanley@centrexin.com",
					Package = new Package { Name = "Pratt@latest", Type = PackageType.Npm },
					SubmittedOn = DateTime.Parse("2018-02-10T16:20:02"),
					Status = RequestStatus.Pending,
					Distribution = DistributionType.Broadcust
				},
				new Request {
					Id = (++id).ToString(),
					Key = new Guid("a0d49275-c3b8-4f6c-9228-82231e6eb5c8"),
					User = "Yates.Houston\\ISOTRONIC",
					Email = "carlson.perry@centree.com",
					Package = new Package { Name = "Figueroa@latest", Type = PackageType.Npm },
					SubmittedOn = DateTime.Parse("2018-03-10T21:19:35"),
					Status = RequestStatus.InProgress,
					Distribution = DistributionType.Broadcust
				},
				new Request {
					Id = (++id).ToString(),
					Key = new Guid("9f75a9ae-f6b0-4c6d-8343-9d454be06df2"),
					User = "Rhodes.Bishop\\NEWCUBE",
					Email = "evelyn.stanley@centrexin.com",
					Package = new Package { Name = "qqratt@latest", Type = PackageType.Npm },
					SubmittedOn = DateTime.Parse("2018-02-10T16:20:02"),
					Status = RequestStatus.InProgress,
					Distribution = DistributionType.Broadcust
				}
			};
			return requestData.ToDictionary(r => r.Key.ToString("B"), r => r);
		}
	}
}
