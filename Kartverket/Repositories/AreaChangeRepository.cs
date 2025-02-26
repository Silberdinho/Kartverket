using Microsoft.EntityFrameworkCore;
using Kartverket.Data;
using Kartverket.Models.DomainModels;

namespace Kartverket.Repositiories
{
    /// <summary>
    /// Repository for managing AreaChange entities in the database
    /// </summary>
    public class AreaChangeRepository : IAreaChangeRepository
    {
        private readonly AppDbContext appDbContext;


        /// <summary>
        /// Initializes a new instance of the <see cref="cref=AreaChangeRepository"/> class.
        /// </summary>
        /// <param name="appDbContext">The database context to be used</param>
        public AreaChangeRepository(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }


        /// <summary>
        /// Adds a new area change 
        /// </summary>
        /// <param name="areaChange">The area change record to add</param>
        /// <returns>The added area change record</returns>
        public async Task<AreaChangeModel> AddAsync(AreaChangeModel areaChange)
        {
            await appDbContext.AreaChanges.AddAsync(areaChange);
            await appDbContext.SaveChangesAsync();
            return areaChange;
        }


        /// <summary>
        /// Deletes an area change record by ID
        /// </summary>
        /// <param name="id">The ID of the area change record to delete.</param>
        /// <returns>The deleted area change record if found, otherwise null</returns>
        public async Task<AreaChangeModel?> DeleteAsync(int id)
        {
            var areaChangeRepository = await appDbContext.AreaChanges.FindAsync(id);
            if (areaChangeRepository == null)
            {
                return null;
            }
            appDbContext.AreaChanges.Remove(areaChangeRepository);
            await appDbContext.SaveChangesAsync();
            return areaChangeRepository;
        }


        /// <summary>
        /// Retrieves all area change records
        /// </summary>
        /// <returns>A list of all area change records</returns>
        public async Task<IEnumerable<AreaChangeModel>> GetAllAsync()
        {
            return await appDbContext.AreaChanges.Include(s => s.SubmitStatusModel).ToListAsync();
        }


        /// <summary>
        /// Finds an area change case by its ID
        /// </summary>
        /// <param name="id">The ID of the area change case</param>
        /// <returns>The area change record if found, otherwise null</returns>
        public async Task<AreaChangeModel?> FindCaseById(int id)
        {
            return await appDbContext.
                AreaChanges.Include(s => s.SubmitStatusModel)
                .Where(x => x.Id == id).FirstOrDefaultAsync();
        }


        /// <summary>
        /// Updates an existing area change record
        /// </summary>
        /// <param name="areaChange">The area change to update</param>
        /// <returns>The updated area change</returns>
        public async Task<AreaChangeModel?> UpdateAsync(AreaChangeModel areaChange)
        {

            appDbContext.AreaChanges.Update(areaChange);
            await appDbContext.SaveChangesAsync();
            return areaChange;
        }
    }
}