﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace RequestManager.Model
{
    public static class Dummy
    {
        public static Dictionary<int, Request> Requests => requests.Value;

        private static Lazy<Dictionary<int, Request>> requests = new Lazy<Dictionary<int, Request>>(GetRequests);

        private static Dictionary<int, Request> GetRequests()
        {
            List<Request> requsetData = new List<Request>{
                new Request
                {
                    Id = 1,
                    CorrelationKey = new Guid("806a59cd-6356-411e-8208-fbd25f02673c"),
                    User = "Snow.Short\\EPLODE",
                    Email = "ingrid.lawson@genmom.com",
                    Package = new Package { Name = "Boone@latest", Type = PackageType.Angular },
                    SubmittedOn = DateTime.Parse("2018-04-21T02:21:31"),
                    Status = RequestStatus.Pending,
                    Distribution = DistributionType.Broadcust
                },
                new Request
                {
                    Id = 2,
                    CorrelationKey = new Guid("ae6c179d-854b-4d12-9dc6-20e8379fd96b"),
                    User = "Maynard.Le\\CONJURICA",
                    Email = "cara.gates@ersum.com",
                    Package = new Package { Name = "Mooney@latest", Type = PackageType.Angular },
                    SubmittedOn = DateTime.Parse("2018-08-22T04:33:15"),
                    Status = RequestStatus.Pending,
                    Distribution = DistributionType.Broadcust
                },
                new Request {
                    Id = 3,
                    CorrelationKey = new Guid("e6ae464b-f324-408e-8606-f2be6f65f551"),
                    User = "Adkins.Britt\\UNIWORLD",
                    Email = "huff.mendoza@surelogic.com",
                    Package = new Package { Name = "Ramsey@latest", Type = PackageType.Nuget },
                    SubmittedOn = DateTime.Parse("2018-01-19T23:00:16"),
                    Status = RequestStatus.Pending,
                    Distribution = DistributionType.Broadcust
                },
                new Request {
                    Id = 4,
                    CorrelationKey = new Guid("099796ea-160b-4a23-a3a4-d3753a439eb8"),
                    User = "Cristina.Justice\\MAROPTIC",
                    Email = "latoya.gonzalez@thredz.com",
                    Package = new Package { Name = "Mercado@latest", Type = PackageType.Nuget },
                    SubmittedOn = DateTime.Parse("2018-03-20T23:55:46"),
                    Status = RequestStatus.Pending,
                    Distribution = DistributionType.Silent
                },
                new Request {
                    Id = 5,
                    CorrelationKey = new Guid("a0d59275-c3b8-4f6c-9228-82231e6eb5c8"),
                    User = "Yates.Houston\\ISOTRONIC",
                    Email = "carlson.perry@centree.com",
                    Package = new Package { Name = "Figueroa@latest", Type = PackageType.Angular },
                    SubmittedOn = DateTime.Parse("2018-03-10T21:19:35"),
                    Status = RequestStatus.Pending,
                    Distribution = DistributionType.Broadcust
                },
                new Request {
                    Id = 6,
                    CorrelationKey = new Guid("9f75a9ae-f6b0-4c6d-8333-9d454be06df2"),
                    User = "Rhodes.Bishop\\NEWCUBE",
                    Email = "evelyn.stanley@centrexin.com",
                    Package = new Package { Name = "Pratt@latest", Type = PackageType.Angular },
                    SubmittedOn = DateTime.Parse("2018-02-10T16:20:02"),
                    Status = RequestStatus.Pending,
                    Distribution = DistributionType.Broadcust
                },
                new Request {
                    Id = 7,
                    CorrelationKey = new Guid("a0d49275-c3b8-4f6c-9228-82231e6eb5c8"),
                    User = "Yates.Houston\\ISOTRONIC",
                    Email = "carlson.perry@centree.com",
                    Package = new Package { Name = "Figueroa@latest", Type = PackageType.Angular },
                    SubmittedOn = DateTime.Parse("2018-03-10T21:19:35"),
                    Status = RequestStatus.InProgress,
                    Distribution = DistributionType.Broadcust
                },
                new Request {
                    Id = 8,
                    CorrelationKey = new Guid("9f75a9ae-f6b0-4c6d-8343-9d454be06df2"),
                    User = "Rhodes.Bishop\\NEWCUBE",
                    Email = "evelyn.stanley@centrexin.com",
                    Package = new Package { Name = "qqratt@latest", Type = PackageType.Angular },
                    SubmittedOn = DateTime.Parse("2018-02-10T16:20:02"),
                    Status = RequestStatus.InProgress,
                    Distribution = DistributionType.Broadcust
                }
            };
            return requsetData.ToDictionary(r => r.Id, r => r);
        }
    }
}