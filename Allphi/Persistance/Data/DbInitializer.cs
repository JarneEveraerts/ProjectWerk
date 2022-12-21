using Domain.Models;

namespace Persistance.Data;

public class DbInitializer
{
    private readonly AllphiContext _allphiContext;

    public DbInitializer(AllphiContext context)
    {
        _allphiContext = context;
    }

    public void Initialize()
    {
        _allphiContext.Database.EnsureCreated();

        if (_allphiContext.Business.Count().Equals(0))
        {
            var businesses = new Business[]
            {
                new Business("Allphi", "BE0838576480", "Allphi@gmail.com", "Guldensporenpark 24, 9820 Merelbeke",
                    "09 396 11 30"),
                new Business("DigiDev", "BE0895415264", "DigiDev@gmail.com", "RandomAdress 85, 9820 Merelbeke",
                    "09 584 64 59")
            };
            var employees = new Employee[]
            {
                new Employee("Everaerts", "Jarne", "Project Manager", businesses[1], "jarne.everaerts@student.hogent.be", "1ABC123"),
                new Employee("De Croock", "Robin", "Senior Designer", businesses[1], "robin.decroock@student.hogent.be", "2DEF456"),
                new Employee("De Meersman", "Lucas", "Senior Developer", businesses[1],
                    "lucas.demeersman@student.hogent.be", null),
                new Employee("De Moor", "Iwein", "Junior Developer", businesses[1], null, null),
                new Employee("De Smet", "Bart", "Junior Developer", businesses[0], null, null),
                new Employee("De Vos", "Marijke", "Junior Developer", businesses[0], null, "1ABB123"),
            };
            var contract = new Contract(businesses[1], DateTime.Now, DateTime.Now.AddYears(1), 10);
            foreach (var busines in businesses)
            {
                _allphiContext.Business.Add(busines);
            }

            _allphiContext.SaveChanges();
            foreach (var employee in employees)
            {
                _allphiContext.Employee.Add(employee);
            }

            _allphiContext.Contract.Add(contract);
            _allphiContext.SaveChanges();
        }
    }
}