using Kartverket.Models.DomainModels;

namespace Kartverket.Repositiories
{
    /// <summary>
    /// Interface for managing area change records in the database
    /// </summary>
    public interface IAreaChangeRepository
    {
        /// <summary>
        /// Retrieves all area change records 
        /// </summary>
        /// <returns>A list of all area change records</returns>
        Task<IEnumerable<AreaChangeModel>> GetAllAsync();

        /// <summary>
        /// Finds an area change case by its ID
        /// </summary>
        /// <param name="id">The ID of the area change case</param>
        /// <returns>The area change record if found, otherwise null.</returns>
        Task<AreaChangeModel?> FindCaseById(int id);

        /// <summary>
        /// Adds a new area change record
        /// </summary>
        /// <param name="areaChangeRepository">The area change record to add</param>
        /// <returns>The added area change record</returns>
        Task<AreaChangeModel> AddAsync(AreaChangeModel areaChangeRepository);

        /// <summary>
        /// Updates an existing area change
        /// </summary>
        /// <param name="areaChangeRepository">The area change record to update</param>
        /// <returns>Tbe updated area change record if found, otherwise null</returns>
        Task<AreaChangeModel?> UpdateAsync(AreaChangeModel areaChangeRepository);

        /// <summary>
        /// Deletes an area change record
        /// </summary>
        /// <param name="id">The ID of the area change record to delete</param>
        /// <returns>The deleted area change record if found, otherwise null</returns>
        Task<AreaChangeModel?> DeleteAsync(int id);

    }
}