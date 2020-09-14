using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CostaRicaApi.Models;
using CostaRicaApi.Repositories;

namespace CostaRicaApi.Services {
    public class StatisticsService : IStatisticsService
    {
        private readonly IExpenseRepository _expenseRepo;

        public List<Expense> Expenses { get; set; }

        public StatisticsService(IExpenseRepository expenseRepo)
        {
            _expenseRepo = expenseRepo;
            Expenses = _expenseRepo.GetAllExpensesAsync().Result;
        }

        public float GetSumOfExpenses()
        {
            //var expenses = await _expenseRepo.GetAllExpensesAsync();
            return Expenses.Sum(e => e.Amount);
        }

        public float GetSumOfExpensesOfThisMonth()
        {
            //var expenses = await _expenseRepo.GetAllExpensesAsync();
            return Expenses.Where(e => {
                    return e.CreatedAt.Year == DateTime.Now.Year && e.CreatedAt.Month == DateTime.Now.Month; 
                }).Sum(e => e.Amount);
        }

        public float GetSumOfExpensesOfThisYear()
        {
            //var expenses = await _expenseRepo.GetAllExpensesAsync();
            return Expenses.Where(e => e.CreatedAt.Year == DateTime.Now.Year).Sum(e => e.Amount);
        }
    }
}