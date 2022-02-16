using System.Data;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;

namespace CoinApi.Domain.Common.Interfaces
{
    /// <summary>
    /// Fluent builder service to execute transactional actions
    /// </summary>
    public interface IFluentActionService
    {
        IDbContextTransaction Transaction { get; }
        
        /// <summary>
        /// Execute action
        /// </summary>
        /// <returns></returns>
        Task ExecuteAsync();
        
        /// <summary>
        /// Specify if a sql transaction needs to be used
        /// </summary>
        /// <param name="isolationLevel">Isolation Level</param>
        /// <returns></returns>
        IFluentActionService WithTransaction(IsolationLevel isolationLevel = IsolationLevel.Serializable);
    }
}