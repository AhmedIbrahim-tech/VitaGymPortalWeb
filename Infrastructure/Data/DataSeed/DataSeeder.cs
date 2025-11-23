using Infrastructure.Entities.Users;

namespace Infrastructure.Data.DataSeed;

public static class DataSeeder
{
    public static async Task<bool> SeedDataAsync(ApplicationDbContext context)
    {
        try
        {
            // Seed Categories
            if (!context.Categories.Any())
            {
                var categoryFaker = new Faker<Category>()
                    .RuleFor(c => c.CategoryName, f => f.Commerce.Categories(1)[0])
                    .RuleFor(c => c.CreatedAt, f => f.Date.Past(2));

                var categories = categoryFaker.Generate(10);
                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Seed Plans
            if (!context.Plans.Any())
            {
                var planFaker = new Faker<Plan>()
                    .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                    .RuleFor(p => p.Description, f => f.Lorem.Sentence())
                    .RuleFor(p => p.Price, f => f.Finance.Amount(50, 500, 2))
                    .RuleFor(p => p.DurationDays, f => f.Random.Int(30, 365))
                    .RuleFor(p => p.IsActive, f => f.Random.Bool(0.8f))
                    .RuleFor(p => p.CreatedAt, f => f.Date.Past(1));

                var plans = planFaker.Generate(3);
                await context.Plans.AddRangeAsync(plans);
                await context.SaveChangesAsync();
            }

            // Seed Trainers
            if (!context.Trainers.Any())
            {
                var trainerFaker = new Faker<Trainer>()
                    .RuleFor(t => t.Name, f => f.Name.FullName().Length > 50 ? f.Name.FullName().Substring(0, 50) : f.Name.FullName())
                    .RuleFor(t => t.Email, f => f.Internet.Email())
                    .RuleFor(t => t.Phone, f => GenerateEgyptianPhoneNumber(f))
                    .RuleFor(t => t.DateOfBirth, f => f.Date.Between(DateTime.Now.AddYears(-50), DateTime.Now.AddYears(-25)))
                    .RuleFor(t => t.Gender, f => f.PickRandom<Gender>())
                    .RuleFor(t => t.Speciality, f => f.PickRandom<Speicalites>())
                    .RuleFor(t => t.Address, f => new Address
                    {
                        Street = f.Address.StreetAddress().Length > 30 ? f.Address.StreetAddress().Substring(0, 30) : f.Address.StreetAddress(),
                        City = f.Address.City().Length > 30 ? f.Address.City().Substring(0, 30) : f.Address.City(),
                        BuildingNumber = f.Address.BuildingNumber()
                    })
                    .RuleFor(t => t.CreatedAt, f => f.Date.Past(1));

                var trainers = trainerFaker.Generate(10);
                await context.Trainers.AddRangeAsync(trainers);
                await context.SaveChangesAsync();
            }

            // Seed Members
            if (!context.Members.Any())
            {
                var memberFaker = new Faker<Member>()
                    .RuleFor(m => m.Name, f => f.Name.FullName().Length > 50 ? f.Name.FullName().Substring(0, 50) : f.Name.FullName())
                    .RuleFor(m => m.Email, f => f.Internet.Email())
                    .RuleFor(m => m.Phone, f => GenerateEgyptianPhoneNumber(f))
                    .RuleFor(m => m.DateOfBirth, f => f.Date.Between(DateTime.Now.AddYears(-60), DateTime.Now.AddYears(-18)))
                    .RuleFor(m => m.Gender, f => f.PickRandom<Gender>())
                    .RuleFor(m => m.Photo, f => $"members/{f.System.FileName("jpg")}")
                    .RuleFor(m => m.Address, f => new Address
                    {
                        Street = f.Address.StreetAddress().Length > 30 ? f.Address.StreetAddress().Substring(0, 30) : f.Address.StreetAddress(),
                        City = f.Address.City().Length > 30 ? f.Address.City().Substring(0, 30) : f.Address.City(),
                        BuildingNumber = f.Address.BuildingNumber()
                    })
                    .RuleFor(m => m.CreatedAt, f => f.Date.Past(1));

                var members = memberFaker.Generate(10);
                await context.Members.AddRangeAsync(members);
                await context.SaveChangesAsync();

                // Seed HealthRecords (one-to-one with Members, using same Id)
                var healthRecordFaker = new Faker<HealthRecord>()
                    .RuleFor(hr => hr.Height, f => f.Random.Decimal(150, 200))
                    .RuleFor(hr => hr.Weight, f => f.Random.Decimal(50, 120))
                    .RuleFor(hr => hr.BloodType, f => f.PickRandom("A+", "A-", "B+", "B-", "AB+", "AB-", "O+", "O-"))
                    .RuleFor(hr => hr.Note, f => f.Random.Bool(0.3f) ? f.Lorem.Sentence() : null)
                    .RuleFor(hr => hr.CreatedAt, f => f.Date.Past(1));

                var healthRecords = new List<HealthRecord>();
                foreach (var member in members)
                {
                    var healthRecord = healthRecordFaker.Generate();
                    healthRecord.Id = member.Id; // One-to-one relationship using same Id
                    healthRecords.Add(healthRecord);
                }

                await context.HealthRecords.AddRangeAsync(healthRecords);
                await context.SaveChangesAsync();
            }

            // Seed Sessions (depends on Categories and Trainers)
            if (!context.Sessions.Any())
            {
                var categories = await context.Categories.ToListAsync();
                var trainers = await context.Trainers.ToListAsync();

                if (categories.Any() && trainers.Any())
                {
                    var sessionFaker = new Faker<Session>()
                        .RuleFor(s => s.Description, f => 
                        {
                            var desc = f.Lorem.Paragraph();
                            // Ensure description is at least 10 characters (validator requirement)
                            while (desc.Length < 10)
                            {
                                desc += " " + f.Lorem.Sentence();
                            }
                            return desc;
                        })
                        .RuleFor(s => s.Capacity, f => f.Random.Int(1, 25))
                        .RuleFor(s => s.StartDate, f => f.Date.Future(1))
                        .RuleFor(s => s.EndDate, (f, s) => s.StartDate.AddHours(f.Random.Int(1, 3)))
                        .RuleFor(s => s.CategoryId, f => f.PickRandom(categories).Id)
                        .RuleFor(s => s.TrainerId, f => f.PickRandom(trainers).Id)
                        .RuleFor(s => s.CreatedAt, f => f.Date.Past(1));

                    var sessions = sessionFaker.Generate(10);
                    await context.Sessions.AddRangeAsync(sessions);
                    await context.SaveChangesAsync();
                }
            }

            // Seed Bookings (depends on Members and Sessions)
            if (!context.Bookings.Any())
            {
                var members = await context.Members.ToListAsync();
                var sessions = await context.Sessions.ToListAsync();

                if (members.Any() && sessions.Any())
                {
                    var bookings = new List<Booking>();
                    var usedCombinations = new HashSet<(int MemberId, int SessionId)>();
                    var faker = new Faker();

                    // Generate unique bookings (composite key: MemberId + SessionId)
                    while (bookings.Count < 10 && usedCombinations.Count < members.Count * sessions.Count)
                    {
                        var member = faker.PickRandom(members);
                        var session = faker.PickRandom(sessions);
                        var combination = (member.Id, session.Id);

                        if (!usedCombinations.Contains(combination))
                        {
                            usedCombinations.Add(combination);
                            bookings.Add(new Booking
                            {
                                MemberId = member.Id,
                                SessionId = session.Id,
                                IsAttended = faker.Random.Bool(0.7f),
                                CreatedAt = faker.Date.Past(1)
                            });
                        }
                    }

                    await context.Bookings.AddRangeAsync(bookings);
                    await context.SaveChangesAsync();
                }
            }

            // Seed MemberShips (depends on Members and Plans)
            if (!context.MemberShips.Any())
            {
                var members = await context.Members.ToListAsync();
                var plans = await context.Plans.ToListAsync();

                if (members.Any() && plans.Any())
                {
                    var membershipFaker = new Faker<MemberShip>()
                        .RuleFor(ms => ms.MemberId, f => f.PickRandom(members).Id)
                        .RuleFor(ms => ms.PlanId, f => f.PickRandom(plans).Id)
                        .RuleFor(ms => ms.EndDate, f => f.Date.Future(1))
                        .RuleFor(ms => ms.CreatedAt, f => f.Date.Past(1));

                    var memberships = membershipFaker.Generate(10);
                    await context.MemberShips.AddRangeAsync(memberships);
                    await context.SaveChangesAsync();
                }
            }

            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    private static string GenerateEgyptianPhoneNumber(Faker f)
    {
        // Phone must start with 010, 011, 012, or 015 and be 11 digits total
        var prefixes = new[] { "010", "011", "012", "015" };
        var prefix = f.PickRandom(prefixes);
        var suffix = f.Random.Int(10000000, 99999999).ToString(); // 8 digits
        return prefix + suffix;
    }
}

