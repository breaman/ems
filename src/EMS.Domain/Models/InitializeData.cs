using EMS.Domain.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Domain.Models
{
    public static class InitializeData
    {
        public static void Initialize(IServiceProvider serviceProvider, ILoggerFactory loggerFactory, string tournament)
        {
            var context = serviceProvider.GetRequiredService<ApplicationDbContext>();
            ILogger logger = loggerFactory.CreateLogger("InitializeData");

            context.Database.Migrate();

            if (!context.Roles.Any())
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<Role>>();
                var createTask = roleManager.CreateAsync(new Role { Name = "Manager" });
                createTask.Wait();
                var identityResult = createTask.Result;

                if (!identityResult.Succeeded)
                {
                    foreach (var error in identityResult.Errors)
                    {
                        logger.LogError($"{error.Code}: {error.Description}");
                    }
                }
                createTask = roleManager.CreateAsync(new Role { Name = "Administrator" });
                createTask.Wait();
                identityResult = createTask.Result;

                if (!identityResult.Succeeded)
                {
                    foreach (var error in identityResult.Errors)
                    {
                        logger.LogError($"{error.Code}: {error.Description}");
                    }
                }
            }

            switch (tournament)
            {
                case "see3slam":
                    InitializeSee3Slam(context);
                    break;
                case "barefoot3v3":
                    InitializeBarefoot3v3(context);
                    break;
                default:
                    break;
            }
        }

        private static void InitializeSee3Slam(ApplicationDbContext context)
        {
            if (!context.TournamentParameters.Any())
            {
                context.TournamentParameters.Add(new TournamentParameter
                {
                    Name = "RegistrationCloses",
                    Value = new DateTimeOffset(new DateTime(2018, 7, 3)).ToString()
                });
                context.TournamentParameters.Add(new TournamentParameter
                {
                    Name = "TournamentDate",
                    Value = new DateTimeOffset(new DateTime(2018, 7, 14)).ToString()
                });
                context.SaveChangesAsync().Wait();
            }
            if (!context.Divisions.Any())
            {
                context.Divisions.Add(new Division { Name = "Over 6 Feet", MinimumNumberOfParticipants = 3, MaximumNumberOfParticipants = 4, IsHeightBased = true, HeightLowerLimit = 0, HeightUpperLimit = 118, Cost = 100, DisplayOrder = 1 });
                context.Divisions.Add(new Division { Name = "6 Feet and Under", MinimumNumberOfParticipants = 3, MaximumNumberOfParticipants = 4, IsHeightBased = true, HeightLowerLimit = 0, HeightUpperLimit = 72, Cost = 100, DisplayOrder = 2 });
                //context.Divisions.Add(new Division { Name = "Open", MinimumNumberOfParticipants = 3, MaximumNumberOfParticipants = 4, Cost = 133, DisplayOrder = 3 });
                context.Divisions.Add(new Division { Name = "High School - Over 6 Feet", MinimumNumberOfParticipants = 3, MaximumNumberOfParticipants = 4, IsHeightBased = true, IsGradeBased = true, HeightLowerLimit = 0, HeightUpperLimit = 118, GradeLowerLimit = 9, GradeUpperLimit = 12, Cost = 100, DisplayOrder = 4 });
                context.Divisions.Add(new Division { Name = "High School - 6 Feet and Under", MinimumNumberOfParticipants = 3, MaximumNumberOfParticipants = 4, IsHeightBased = true, IsGradeBased = true, HeightLowerLimit = 0, HeightUpperLimit = 72, GradeLowerLimit = 9, GradeUpperLimit = 12, Cost = 100, DisplayOrder = 5 });
                context.Divisions.Add(new Division { Name = "Grade 1-2", MinimumNumberOfParticipants = 3, MaximumNumberOfParticipants = 4, IsGradeBased = true, GradeLowerLimit = 1, GradeUpperLimit = 2, Cost = 100, DisplayOrder = 6 });
                context.Divisions.Add(new Division { Name = "Grade 3-4", MinimumNumberOfParticipants = 3, MaximumNumberOfParticipants = 4, IsGradeBased = true, GradeLowerLimit = 3, GradeUpperLimit = 4, Cost = 100, DisplayOrder = 7 });
                context.Divisions.Add(new Division { Name = "Grade 5-6", MinimumNumberOfParticipants = 3, MaximumNumberOfParticipants = 4, IsGradeBased = true, GradeLowerLimit = 5, GradeUpperLimit = 6, Cost = 100, DisplayOrder = 8 });
                context.Divisions.Add(new Division { Name = "Grade 7-8", MinimumNumberOfParticipants = 3, MaximumNumberOfParticipants = 4, IsGradeBased = true, GradeLowerLimit = 7, GradeUpperLimit = 8, Cost = 100, DisplayOrder = 9 });
                // context.Divisions.Add(new Division { Name = "Wheelchair", MinimumNumberOfParticipants = 3, MaximumNumberOfParticipants = 4, Cost = 133, DisplayOrder = 10 });
                // context.Divisions.Add(new Division { Name = "Special Olympics", MinimumNumberOfParticipants = 3, MaximumNumberOfParticipants = 4, Cost = 133, DisplayOrder = 11 });
                // context.Divisions.Add(new Division { Name = "Unified", MinimumNumberOfParticipants = 3, MaximumNumberOfParticipants = 4, Cost = 133, DisplayOrder = 12 });
                context.Divisions.Add(new Division { Name = "Over 35", MinimumNumberOfParticipants = 3, MaximumNumberOfParticipants = 4, Cost = 100, DisplayOrder = 3 });

                context.SaveChangesAsync().Wait();
            }
            if (!context.Genders.Any())
            {
                var divisions = context.Divisions.ToList();
                context.Genders.Add(new Gender { Name = "Co-Ed", DisplayOrder = 1, DivisionId = divisions.SingleOrDefault(d => d.Name == "Over 6 Feet").Id });
                context.Genders.Add(new Gender { Name = "Male", DisplayOrder = 2, DivisionId = divisions.SingleOrDefault(d => d.Name == "Over 6 Feet").Id });
                context.Genders.Add(new Gender { Name = "Female", DisplayOrder = 3, DivisionId = divisions.SingleOrDefault(d => d.Name == "Over 6 Feet").Id });
                context.Genders.Add(new Gender { Name = "Co-Ed", DisplayOrder = 1, DivisionId = divisions.SingleOrDefault(d => d.Name == "6 Feet and Under").Id });
                context.Genders.Add(new Gender { Name = "Male", DisplayOrder = 2, DivisionId = divisions.SingleOrDefault(d => d.Name == "6 Feet and Under").Id });
                context.Genders.Add(new Gender { Name = "Female", DisplayOrder = 3, DivisionId = divisions.SingleOrDefault(d => d.Name == "6 Feet and Under").Id });
                //context.Genders.Add(new Gender { Name = "Co-Ed", DisplayOrder = 1, DivisionId = divisions.SingleOrDefault(d => d.Name == "High School - Over 6 Feet").Id });
                context.Genders.Add(new Gender { Name = "Male", DisplayOrder = 2, DivisionId = divisions.SingleOrDefault(d => d.Name == "High School - Over 6 Feet").Id });
                context.Genders.Add(new Gender { Name = "Female", DisplayOrder = 3, DivisionId = divisions.SingleOrDefault(d => d.Name == "High School - Over 6 Feet").Id });
                //context.Genders.Add(new Gender { Name = "Co-Ed", DisplayOrder = 1, DivisionId = divisions.SingleOrDefault(d => d.Name == "High School - 6 Feet and Under").Id });
                context.Genders.Add(new Gender { Name = "Male", DisplayOrder = 2, DivisionId = divisions.SingleOrDefault(d => d.Name == "High School - 6 Feet and Under").Id });
                context.Genders.Add(new Gender { Name = "Female", DisplayOrder = 3, DivisionId = divisions.SingleOrDefault(d => d.Name == "High School - 6 Feet and Under").Id });
                context.Genders.Add(new Gender { Name = "Male", DisplayOrder = 2, DivisionId = divisions.SingleOrDefault(d => d.Name == "Grade 1-2").Id });
                context.Genders.Add(new Gender { Name = "Female", DisplayOrder = 3, DivisionId = divisions.SingleOrDefault(d => d.Name == "Grade 1-2").Id });
                //context.Genders.Add(new Gender { Name = "Co-Ed", DisplayOrder = 1, DivisionId = divisions.SingleOrDefault(d => d.Name == "Grade 3-4").Id });
                context.Genders.Add(new Gender { Name = "Male", DisplayOrder = 2, DivisionId = divisions.SingleOrDefault(d => d.Name == "Grade 3-4").Id });
                context.Genders.Add(new Gender { Name = "Female", DisplayOrder = 3, DivisionId = divisions.SingleOrDefault(d => d.Name == "Grade 3-4").Id });
                //context.Genders.Add(new Gender { Name = "Co-Ed", DisplayOrder = 1, DivisionId = divisions.SingleOrDefault(d => d.Name == "Grade 5-6").Id });
                context.Genders.Add(new Gender { Name = "Male", DisplayOrder = 2, DivisionId = divisions.SingleOrDefault(d => d.Name == "Grade 5-6").Id });
                context.Genders.Add(new Gender { Name = "Female", DisplayOrder = 3, DivisionId = divisions.SingleOrDefault(d => d.Name == "Grade 5-6").Id });
                //context.Genders.Add(new Gender { Name = "Co-Ed", DisplayOrder = 1, DivisionId = divisions.SingleOrDefault(d => d.Name == "Grade 7-8").Id });
                context.Genders.Add(new Gender { Name = "Male", DisplayOrder = 2, DivisionId = divisions.SingleOrDefault(d => d.Name == "Grade 7-8").Id });
                context.Genders.Add(new Gender { Name = "Female", DisplayOrder = 3, DivisionId = divisions.SingleOrDefault(d => d.Name == "Grade 7-8").Id });
                // context.Genders.Add(new Gender { Name = "Co-Ed", DisplayOrder = 1, DivisionId = divisions.SingleOrDefault(d => d.Name == "Special Olympics").Id });
                // context.Genders.Add(new Gender { Name = "Male", DisplayOrder = 2, DivisionId = divisions.SingleOrDefault(d => d.Name == "Special Olympics").Id });
                // context.Genders.Add(new Gender { Name = "Female", DisplayOrder = 3, DivisionId = divisions.SingleOrDefault(d => d.Name == "Special Olympics").Id });
                // context.Genders.Add(new Gender { Name = "Co-Ed", DisplayOrder = 1, DivisionId = divisions.SingleOrDefault(d => d.Name == "Wheelchair").Id });
                // context.Genders.Add(new Gender { Name = "Male", DisplayOrder = 2, DivisionId = divisions.SingleOrDefault(d => d.Name == "Wheelchair").Id });
                // context.Genders.Add(new Gender { Name = "Female", DisplayOrder = 3, DivisionId = divisions.SingleOrDefault(d => d.Name == "Wheelchair").Id });
                // context.Genders.Add(new Gender { Name = "Co-Ed", DisplayOrder = 1, DivisionId = divisions.SingleOrDefault(d => d.Name == "Unified").Id });
                // context.Genders.Add(new Gender { Name = "Male", DisplayOrder = 2, DivisionId = divisions.SingleOrDefault(d => d.Name == "Unified").Id });
                // context.Genders.Add(new Gender { Name = "Female", DisplayOrder = 3, DivisionId = divisions.SingleOrDefault(d => d.Name == "Unified").Id });
                context.Genders.Add(new Gender { Name = "Co-Ed", DisplayOrder = 1, DivisionId = divisions.SingleOrDefault(d => d.Name == "Over 35").Id });
                context.Genders.Add(new Gender { Name = "Male", DisplayOrder = 2, DivisionId = divisions.SingleOrDefault(d => d.Name == "Over 35").Id });
                context.Genders.Add(new Gender { Name = "Female", DisplayOrder = 3, DivisionId = divisions.SingleOrDefault(d => d.Name == "Over 35").Id });

                context.SaveChangesAsync().Wait();
            }
            if (!context.CompetitionLevels.Any())
            {
                var divisions = context.Divisions.ToList();
                var genders = context.Genders.ToList();

                var divisionId = divisions.SingleOrDefault(d => d.Name == "Over 6 Feet").Id;
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Competitive", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 2, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Co-Ed").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Competitive", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 2, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });

                divisionId = divisions.SingleOrDefault(d => d.Name == "6 Feet and Under").Id;
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Competitive", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 2, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Competitive", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Co-Ed").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 2, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Co-Ed").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Competitive", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 2, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });

                //divisionId = divisions.SingleOrDefault(d => d.Name == "Open").Id;
                //context.CompetitionLevels.Add(new CompetitionLevel { Name = "Competitive", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });
                //context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 2, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });

                divisionId = divisions.SingleOrDefault(d => d.Name == "High School - Over 6 Feet").Id;
                //context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Co-Ed").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });

                divisionId = divisions.SingleOrDefault(d => d.Name == "High School - 6 Feet and Under").Id;
                //context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Co-Ed").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });

                divisionId = divisions.SingleOrDefault(d => d.Name == "Grade 1-2").Id;
                //context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Co-Ed").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });

                divisionId = divisions.SingleOrDefault(d => d.Name == "Grade 3-4").Id;
                //context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Co-Ed").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });

                divisionId = divisions.SingleOrDefault(d => d.Name == "Grade 5-6").Id;
                //context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Co-Ed").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });

                divisionId = divisions.SingleOrDefault(d => d.Name == "Grade 7-8").Id;
                //context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Co-Ed").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });

                // divisionId = divisions.SingleOrDefault(d => d.Name == "Special Olympics").Id;
                // context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Co-Ed").Id });
                // context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                // context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });

                // divisionId = divisions.SingleOrDefault(d => d.Name == "Wheelchair").Id;
                // context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Co-Ed").Id });
                // context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                // context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });

                // divisionId = divisions.SingleOrDefault(d => d.Name == "Unified").Id;
                // context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Co-Ed").Id });
                // context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                // context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });

                divisionId = divisions.SingleOrDefault(d => d.Name == "Over 35").Id;
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Co-Ed").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Male").Id });
                context.CompetitionLevels.Add(new CompetitionLevel { Name = "Recreational", DisplayOrder = 1, DivisionId = divisionId, GenderId = genders.SingleOrDefault(g => g.DivisionId == divisionId && g.Name == "Female").Id });

                context.SaveChangesAsync().Wait();
            }

            if (!context.PromoCodes.Any())
            {
                string codeNumber = "";
                for (int i = 1; i <= 300; i++)
                {
                    codeNumber = i.ToString("000");
                    context.PromoCodes.Add(new PromotionalCode { PromoCode = $"WCADp{codeNumber}", FlatRateCost = 100, NumberOfCodes = 1 });
                    context.PromoCodes.Add(new PromotionalCode { PromoCode = $"WCADk{codeNumber}", FlatRateCost = 100, NumberOfCodes = 1 });
                    context.PromoCodes.Add(new PromotionalCode { PromoCode = $"WCADm{codeNumber}", FlatRateCost = 100, NumberOfCodes = 1 });
                }

                for (int i = 1; i <= 99; i++)
                {
                    codeNumber = i.ToString("00");
                    context.PromoCodes.Add(new PromotionalCode { PromoCode = $"WCAD{codeNumber}FREE", FlatRateCost = 0, NumberOfCodes = 1 });
                }

                context.SaveChangesAsync().Wait();
            }
        }

        private static void InitializeBarefoot3v3(ApplicationDbContext context)
        {
            if (!context.TournamentParameters.Any())
            {
                context.TournamentParameters.Add(new TournamentParameter
                {
                    Name = "RegistrationCloses",
                    Value = new DateTimeOffset(new DateTime(2016, 7, 25)).ToString()
                });
                context.TournamentParameters.Add(new TournamentParameter
                {
                    Name = "TournamentDate",
                    Value = new DateTimeOffset(new DateTime(2016, 8, 6)).ToString()
                });
                context.TournamentParameters.Add(new TournamentParameter
                {
                    Name = "RegistrationOpens",
                    Value = new DateTimeOffset(new DateTime(2016, 6, 1)).ToString()
                });
                context.SaveChangesAsync().Wait();
            }
            if (!context.Divisions.Any())
            {
                context.Divisions.Add(new Division { Name = "Catchall", MinimumNumberOfParticipants = 4, MaximumNumberOfParticipants = 5, Cost = 175, DisplayOrder = 1 });

                context.SaveChangesAsync().Wait();
            }

            if (!context.Genders.Any())
            {
                context.Genders.Add(new Gender { Name = "Co-Ed", DisplayOrder = 1, DivisionId = 1 });
                context.Genders.Add(new Gender { Name = "Male", DisplayOrder = 2, DivisionId = 1 });
                context.Genders.Add(new Gender { Name = "Female", DisplayOrder = 3, DivisionId = 1 });

                context.SaveChangesAsync().Wait();
            }

            if (!context.CompetitionLevels.Any())
            {
                var genders = context.Genders.ToList();
                context.CompetitionLevels.Add(new CompetitionLevel
                {
                    Name = "Competitive",
                    DisplayOrder = 1,
                    DivisionId = 1,
                    GenderId = genders.SingleOrDefault(g => g.DivisionId == 1 && g.Name == "Co-Ed").Id
                });

                context.CompetitionLevels.Add(new CompetitionLevel
                {
                    Name = "Competitive",
                    DisplayOrder = 1,
                    DivisionId = 1,
                    GenderId = genders.SingleOrDefault(g => g.DivisionId == 1 && g.Name == "Male").Id
                });

                context.CompetitionLevels.Add(new CompetitionLevel
                {
                    Name = "Competitive",
                    DisplayOrder = 1,
                    DivisionId = 1,
                    GenderId = genders.SingleOrDefault(g => g.DivisionId == 1 && g.Name == "Female").Id
                });

                context.CompetitionLevels.Add(new CompetitionLevel
                {
                    Name = "Recreational",
                    DisplayOrder = 1,
                    DivisionId = 1,
                    GenderId = genders.SingleOrDefault(g => g.DivisionId == 1 && g.Name == "Co-Ed").Id
                });

                context.CompetitionLevels.Add(new CompetitionLevel
                {
                    Name = "Recreational",
                    DisplayOrder = 1,
                    DivisionId = 1,
                    GenderId = genders.SingleOrDefault(g => g.DivisionId == 1 && g.Name == "Male").Id
                });

                context.CompetitionLevels.Add(new CompetitionLevel
                {
                    Name = "Recreational",
                    DisplayOrder = 1,
                    DivisionId = 1,
                    GenderId = genders.SingleOrDefault(g => g.DivisionId == 1 && g.Name == "Female").Id
                });

                context.SaveChangesAsync().Wait();
            }
        }
    }
}
