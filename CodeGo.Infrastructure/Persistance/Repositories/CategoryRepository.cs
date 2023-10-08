

using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CategoryAggregateRoot;
using CodeGo.Domain.CategoryAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CodeGo.Infrastructure.Persistance.Repositories;

public class CategoryRepository : ICategoryRepository
{
    private readonly CodeGoDbContext _dbContext;

    public CategoryRepository(CodeGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Category category)
    {
        await _dbContext.AddAsync(category);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Category?> FindById(CategoryId categoryId)
    {
        return await _dbContext.Categories
            .FirstOrDefaultAsync(category => category.Id == categoryId);
    }

    public async Task<List<Category>> ListAsync()
    {
        return await _dbContext.Categories.ToListAsync();
    }

    public async Task Update(Category category)
    {
        _dbContext.Update(category);
        await _dbContext.SaveChangesAsync();
    }
}
