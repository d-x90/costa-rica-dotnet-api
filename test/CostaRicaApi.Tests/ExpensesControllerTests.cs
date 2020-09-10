using CostaRicaApi.Controllers;
using CostaRicaApi.Models;
using CostaRicaApi.Repositories;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CostaRicaApi.Tests
{
    public class ExpensesControllerTests
    {
        [Fact]
        public async void GetExpenses_ReturnsZeroItems_WhenDBIsEmpty()
        {
            //Arrange
            //DBContext
            var optionsBuilder = new DbContextOptionsBuilder<ExpenseContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestInMemBD");
            var dbContext = new ExpenseContext(optionsBuilder.Options);

            var expenseRepo = new ExpenseRepository(dbContext);

            //Controller
            var controller = new ExpensesController(expenseRepo, null);

            //Act
            var result = await controller.GetExpenses();

            //Assert
            Assert.Empty(result.Value);
        }

        [Fact]
        public async void GetExpenses_ReturnsTwoItems_WhenDBIsNotEmpty()
        {
            //Arrange
            //DBContext
            var optionsBuilder = new DbContextOptionsBuilder<ExpenseContext>();
            optionsBuilder.UseInMemoryDatabase("UnitTestInMemBD2");
            var dbContext = new ExpenseContext(optionsBuilder.Options);
            dbContext.Add(new Expense() {
               Amount = 88.49f,
               Currency = Currency.EUR,
               Description = "Skateboard trucks" 
            });

            dbContext.Add(new Expense() {
               Amount = 49.99f,
               Currency = Currency.EUR,
               Description = "Skateboard deck" 
            });

            dbContext.SaveChanges();

            var expenseRepo = new ExpenseRepository(dbContext);

            //Controller
            var controller = new ExpensesController(expenseRepo);

            //Act
            var result = await controller.GetExpenses();

            //Assert
            Assert.Equal(2, result.Value.Count);
        }
    }
}
