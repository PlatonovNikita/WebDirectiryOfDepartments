using Microsoft.EntityFrameworkCore;
using WebDirectiryOfDepartments.Core.Model;
using WebDirectiryOfDepartments.DataServices.Context;

namespace WebDirectiryOfDepartments.Middleware
{
    public static class SeedDataAppBuilderExtensions
    {
        public static IApplicationBuilder SeedData(this IApplicationBuilder app)
        {
            using var scope = app.ApplicationServices.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<DirectiryOfDepartmentsContext>();

            if (dbContext != null)
            {
                dbContext.Database.Migrate();
                if (!dbContext.DirectoryUnits.Any())
                {
                    var directoryUnits = new[]
                    {
                        new DirectoryUnit
                        {
                            Name = "Компания ДельтаЛизин",
                            ChildDirectoryUnits = new List<DirectoryUnit>
                            {
                                new DirectoryUnit
                                {
                                    Name = "It департамент",
                                    ChildDirectoryUnits = new List<DirectoryUnit>
                                    {
                                        new DirectoryUnit
                                        {
                                            Name = "Отдел разработки программного обеспечения"
                                        },
                                        new DirectoryUnit
                                        {
                                            Name = "Отдел тестирования программного обеспечения"
                                        },
                                        new DirectoryUnit
                                        {
                                            Name = "Отдел обучения и развития персонала"
                                        }
                                    }
                                },
                                new DirectoryUnit
                                {
                                    Name = "Департамент маркетинга",
                                    ChildDirectoryUnits = new List<DirectoryUnit>
                                    {
                                        new DirectoryUnit
                                        {
                                            Name = "Отдел анализа и исследования рынка"
                                        },
                                        new DirectoryUnit
                                        {
                                            Name = "Отдел работы с партнерами и поставщиками"
                                        }
                                    }
                                }
                            }
                        },
                        new DirectoryUnit
                            {
                                Name = "Компания АльфаЛизинг",
                                ChildDirectoryUnits = new List<DirectoryUnit>
                                {
                                    new DirectoryUnit
                                    {
                                        Name = "Департамент экономической безопасности",
                                        ChildDirectoryUnits = new List<DirectoryUnit>
                                        {
                                            new DirectoryUnit
                                            {
                                                Name = "Отдел борьбы с коррупцией и экономическими преступлениями."
                                            },
                                            new DirectoryUnit
                                            {
                                                Name = "Отдел информационной безопасности и защиты экономических данных."
                                            }
                                        }
                                    },
                                    new DirectoryUnit
                                    {
                                        Name = "Департамент рекламы",
                                        ChildDirectoryUnits = new List<DirectoryUnit>
                                        {
                                            new DirectoryUnit
                                            {
                                                Name = "Отдел проведения рекламных кампаний."
                                            },
                                            new DirectoryUnit
                                            {
                                                    Name = "Отдел копирайтинга и контента."
                                            }
                                        }
                                    }
                                }
                            },
                        new DirectoryUnit
                        {
                            Name = "Компания ГаммаЛизинг",
                            ChildDirectoryUnits = new List<DirectoryUnit>
                            {
                                new DirectoryUnit
                                {
                                    Name = "Департамент операционного управления и логистики",
                                    ChildDirectoryUnits = new List<DirectoryUnit>
                                    {
                                        new DirectoryUnit
                                        {
                                            Name = "Отдел логистики и закупок."
                                        },
                                        new DirectoryUnit
                                        {
                                            Name = "Отдел транспортной логистики."
                                        },
                                        new DirectoryUnit
                                        {
                                            Name = "Отдел складского хозяйства."
                                        }
                                    }
                                },
                                new DirectoryUnit
                                {
                                    Name = "Департамент контроля качества услуг",
                                    ChildDirectoryUnits = new List<DirectoryUnit>
                                    {
                                        new DirectoryUnit
                                        {
                                            Name = "Отдел входного контроля качества."
                                        },
                                        new DirectoryUnit
                                        {
                                            Name = "Отдел мониторинга качества услуг конкурентов."
                                        }
                                    }
                                }
                            }
                        }
                    };

                    dbContext.DirectoryUnits.AddRange(directoryUnits);
                    dbContext.SaveChanges();
                }
            }

            return app;
        }
    }
}
