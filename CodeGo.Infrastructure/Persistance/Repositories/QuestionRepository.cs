
using CodeGo.Application.Common.Interfaces.Persistance;
using CodeGo.Domain.CourseAggregateRoot.ValueObjects;
using CodeGo.Domain.QuestionAggregateRoot;
using CodeGo.Domain.QuestionAggregateRoot.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace CodeGo.Infrastructure.Persistance.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly CodeGoDbContext _dbContext;

    public QuestionRepository(CodeGoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<Question>> FindByCourseId(CourseId courseId)
    {
        return await _dbContext.Questions
            .Where(q => q.CourseId == courseId)
            .ToListAsync();
    }

    public async Task Add(Question question)
    {
        await _dbContext.AddAsync(question);
        await _dbContext.SaveChangesAsync();
    }

    public async Task<Question?> FindById(QuestionId questionId)
    {
        return await _dbContext.Questions
            .FirstOrDefaultAsync(question => question.Id == questionId);
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}
