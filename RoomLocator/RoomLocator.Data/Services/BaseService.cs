using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using RoomLocator.Data.Config;
using Shared;

namespace RoomLocator.Data.Services
{
    public abstract class BaseService
    {
        protected readonly RoomLocatorContext _context;
        protected readonly IMapper _mapper;

        protected BaseService(RoomLocatorContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Loads a model from the Database and maps it to the Result Model
        /// </summary>
        /// <param name="query">The IQueryable to search</param>
        /// <param name="filter">The filter for finding the model</param>
        /// <typeparam name="TInput">The type searched for</typeparam>
        /// <typeparam name="TResult">The returned type (mapped)</typeparam>
        /// <exception cref="NotFoundException">Throws exception if model can't be found in DB</exception>
        /// <returns>The requested model</returns>
        protected async Task<TResult> GetModel<TInput, TResult>(
	        IQueryable<TInput> query,
	        Expression<Func<TInput, bool>> filter = null
        ) {
	        return _mapper.Map<TResult>(await GetModel(query, filter));
        }

        /// <summary>
        /// Loads a model from the Database
        /// </summary>
        /// <param name="query">The IQueryable to search</param>
        /// <param name="filter">The filter for finding the model</param>
        /// <typeparam name="TInput">The type searched for</typeparam>
        /// <returns>Any model matching the filter in the query</returns>
        /// <exception cref="NotFoundException">Throws exception if model can't be found in DB</exception>
        protected async Task<TInput> GetModel<TInput>(
	        IQueryable<TInput> query,
	        Expression<Func<TInput, bool>> filter = null
        ) {
	        var model = await (filter == null ? query.FirstOrDefaultAsync() : query.FirstOrDefaultAsync(filter));

	        if (model != null) return model;

	        throw GenerateError(filter);
        }

        protected async Task EnsureExists<TInput>(
	        IQueryable<TInput> query,
	        Expression<Func<TInput, bool>> filter
        ) {
	        var exists = await query.AnyAsync(filter);

	        if (!exists) throw GenerateError(filter);
        }
        
        /// <summary>
		/// Fetches a model. If the model is not found, a model will be created in it's stead
		/// </summary>
		/// <param name="set">DBSet to create and search in</param>
		/// <param name="input">The data for creating a new model</param>
		/// <param name="filter">The filter used to find the model</param>
		/// <param name="modify">A method used to modify the creating data, after having mapped it to the Result type</param>
		/// <param name="save">Indicated if the changes should be saved</param>
		/// <typeparam name="TInput">The type of the supplied data</typeparam>
		/// <typeparam name="TResult">The type of the model</typeparam>
		/// <returns>Returns the fetched model or the created one</returns>
		protected Task<TResult> GetCreateModel<TInput, TResult>(
			DbSet<TResult> set,
			TInput input,
			Expression<Func<TResult, bool>> filter = null,
			Action<TResult> modify = null,
			bool save = true
		) where TResult : class {
			return GetCreateModel(set, set, input, filter, modify, save);
		}

		/// <summary>
		/// Fetches a model. If the model is not found, a model will be created in it's stead
		/// </summary>
		/// <param name="query">IQueryable to search in</param>
		/// <param name="set">DBSet to create in</param>
		/// <param name="input">The data for creating a new model</param>
		/// <param name="filter">The filter used to find the model</param>
		/// <param name="modify">A method used to modify the creating data, after having mapped it to the Result type</param>
		/// <param name="save">Indicated if the changes should be saved</param>
		/// <typeparam name="TInput">The type of the supplied data</typeparam>
		/// <typeparam name="TResult">The type of the model</typeparam>
		/// <returns>Returns the fetched model or the created one</returns>
		protected async Task<TResult> GetCreateModel<TInput, TResult>(
			IQueryable<TResult> query,
			DbSet<TResult> set,
			TInput input,
			Expression<Func<TResult, bool>> filter = null,
			Action<TResult> modify = null,
			bool save = true
		) where TResult : class {
			try {
				return await GetModel(query, filter);
			} catch (NotFoundException) {
				return await CreateModel(set, input, modify, save);
			}
		}

		/// <summary>
		/// Fetches a model. If the model is not found, a model will be created in it's stead. The resulting model is mapped to the TMap type.
		/// </summary>
		/// <param name="query">IQueryable to search in</param>
		/// <param name="set">DBSet to create in</param>
		/// <param name="input">The data for creating a new model</param>
		/// <param name="filter">The filter used to find the model</param>
		/// <param name="modify">A method used to modify the creating data, after having mapped it to the Result type</param>
		/// <param name="save">Indicated if the changes should be saved</param>
		/// <typeparam name="TInput">The type of the supplied data</typeparam>
		/// <typeparam name="TResult">The type of the model</typeparam>
		/// <typeparam name="TMap">The type returned after mapping</typeparam>
		/// <returns>Returns the fetched model or the created one</returns>
		protected async Task<TMap> GetCreateModel<TInput, TResult, TMap>(
			IQueryable<TResult> query,
			DbSet<TResult> set,
			TInput input,
			Expression<Func<TResult, bool>> filter = null,
			Action<TResult> modify = null,
			bool save = true
		) where TResult : class {
			return _mapper.Map<TMap>(await GetCreateModel(query, set, input, filter, modify, save));
		}
		
		/// <summary>
		/// Fetches a model. If the model is not found, a model will be created in it's stead. The resulting model is mapped to the TMap type.
		/// </summary>
		/// <param name="set">DBSet to search and create in</param>
		/// <param name="input">The data for creating a new model</param>
		/// <param name="filter">The filter used to find the model</param>
		/// <param name="modify">A method used to modify the creating data, after having mapped it to the Result type</param>
		/// <param name="save">Indicated if the changes should be saved</param>
		/// <typeparam name="TInput">The type of the supplied data</typeparam>
		/// <typeparam name="T">The type of the model</typeparam>
		/// <typeparam name="TOutput">The type returned after mapping</typeparam>
		/// <returns>Returns the fetched model or the created one</returns>
		protected async Task<TOutput> GetCreateModel<TInput, T, TOutput>(
			DbSet<T> set,
			TInput input,
			Expression<Func<T, bool>> filter = null,
			Action<T> modify = null,
			bool save = true
		) where T : class {
			return _mapper.Map<TOutput>(await GetCreateModel(set, set, input, filter, modify, save));
		}

		/// <summary>
		/// Updates a model in the Database
		/// </summary>
		/// <param name="set">The set where the model resides</param>
		/// <param name="input">The data to change to</param>
		/// <param name="filter">A filter to target a specific model</param>
		/// <param name="modify">A function to modify the model before submitting</param>
		/// <param name="create">Indicates if a non existent model should be created if not found</param>
		/// <typeparam name="TInput">The type of the update data</typeparam>
		/// <typeparam name="TResult">The type of the model</typeparam>
		/// <exception cref="NotFoundException">Throws exception if model can't be found in DB and create is false</exception>
		/// <returns>The updated / created model</returns>
		protected Task<TResult> UpdateModel<TInput, TResult>(
			DbSet<TResult> set,
			TInput input,
			Expression<Func<TResult, bool>> filter = null,
			Action<TResult> modify = null,
			bool create = false
		) where TResult : class {
			return UpdateModel(set.AsQueryable(), input, filter, modify, create ? set : null);
		}

		/// <summary>
		/// Updates a model in the Database, and maps the resulting model to the Map type
		/// </summary>
		/// <param name="set">The set where the model resides</param>
		/// <param name="input">The data to change to</param>
		/// <param name="filter">A filter to target a specific model</param>
		/// <param name="modify">A function to modify the model before submitting</param>
		/// <param name="create">Indicates if a non existent model should be created if not found</param>
		/// <typeparam name="TInput">The type of the update data</typeparam>
		/// <typeparam name="T">The type of the model</typeparam>
		/// <typeparam name="TOutput">The type that the model gets mapped to</typeparam>
		/// <exception cref="NotFoundException">Throws exception if model can't be found in DB and create is false</exception>
		/// <returns>The updated / created model</returns>
		protected async Task<TOutput> UpdateModel<TInput, T, TOutput>(
			DbSet<T> set,
			TInput input,
			Expression<Func<T, bool>> filter,
			Action<T> modify = null,
			bool create = false
		) where T : class {
			return _mapper.Map<TOutput>(await UpdateModel(set, input, filter, modify, create));
		}

		/// <summary>
		/// Updates a model in the Database
		/// </summary>
		/// <param name="query">An IQueryable to search for an existing model</param>
		/// <param name="input">The data to change to</param>
		/// <param name="filter">The filter used for finding the existing model</param>
		/// <param name="modify">A function to modify the model before submitting</param>
		/// <param name="set">A DBSet where a new model is created, given none are found. Leave as null to never create</param>
		/// <typeparam name="TInput">The type of the update data</typeparam>
		/// <typeparam name="TResult">The type of the model</typeparam>
		/// <exception cref="NotFoundException">Throws exception if model can't be found in DB and set is null</exception>
		/// <returns>The updated / created model</returns>
		protected async Task<TResult> UpdateModel<TInput, TResult>(
			IQueryable<TResult> query,
			TInput input,
			Expression<Func<TResult, bool>> filter,
			Action<TResult> modify = null,
			DbSet<TResult> set = null
		) where TResult : class {
			TResult model;

			try {
				model = await GetModel(query, filter);
				_mapper.Map(input, model);
				modify?.Invoke(model);
			} catch (NotFoundException) {
				if (set == null) throw;
				model = await CreateModel(set, input, modify, false);
			}

			await _context.SaveChangesAsync();

			return model;
		}

		/// <summary>
		/// Updates a model in the Database, and maps it to the Map type
		/// </summary>
		/// <param name="query">An IQueryable to search for an existing model</param>
		/// <param name="input">The data to change to</param>
		/// <param name="filter">The filter used for finding the existing model</param>
		/// <param name="modify">A function to modify the model before submitting</param>
		/// <param name="set">A DBSet where a new model is created, given none are found. Leave as null to never create</param>
		/// <typeparam name="TInput">The type of the update data</typeparam>
		/// <typeparam name="TResult">The type of the model</typeparam>
		/// <typeparam name="TMap">The type that the model gets mapped to</typeparam>
		/// <exception cref="NotFoundException">Throws exception if model can't be found in DB and set is null</exception>
		/// <returns>The updated / created model</returns>
		protected async Task<TMap> UpdateModel<TInput, TResult, TMap>(
			IQueryable<TResult> query,
			TInput input,
			Expression<Func<TResult, bool>> filter = null,
			Action<TResult> modify = null,
			DbSet<TResult> set = null
		) where TResult : class {
			return _mapper.Map<TMap>(await UpdateModel(query, input, filter, modify, set));
		}
        
        /// <summary>
        /// Creates a new model in the given set, and maps the result to the Map type
        /// </summary>
        /// <param name="set">The set in which to place the model</param>
        /// <param name="input">The data used to create a new model</param>
        /// <param name="modify">A method to modify data after mapping to Result type</param>
        /// <param name="save">A boolean indicating if the changes should be committed to DB</param>
        /// <typeparam name="TInput">The type of the input model</typeparam>
        /// <typeparam name="T">The type of the model in the DBSet</typeparam>
        /// <typeparam name="TOutput">The type that the model gets mapped to</typeparam>
        /// <returns>The created resource</returns>
        protected async Task<TOutput> CreateModel<TInput, T, TOutput>(
            DbSet<T> set,
            TInput input,
            Action<T> modify = null,
            bool save = true
        ) where T : class {
            return _mapper.Map<TOutput>(await CreateModel(set, input, modify, save));
        }
        
        /// <summary>
        /// Creates a new model in the given set
        /// </summary>
        /// <param name="set">The set in which to place the model</param>
        /// <param name="input">The data used to create a new model</param>
        /// <param name="modify">A method to modify data after mapping to Result type</param>
        /// <param name="save">A boolean indicating if the changes should be committed to DB</param>
        /// <typeparam name="TInput">The type of the input model</typeparam>
        /// <typeparam name="TResult">The type of the model in the DBSet</typeparam>
        /// <returns>The created resource</returns>
        protected async Task<TResult> CreateModel<TInput, TResult>(
            DbSet<TResult> set,
            TInput input,
            Action<TResult> modify = null,
            bool save = true
        ) where TResult : class {
            var data = _mapper.Map<TResult>(input);
            modify?.Invoke(data);

            set.Add(data);
            if (save) await _context.SaveChangesAsync();
            return data;
        }
        
        /// <summary>
        /// Deletes a model from the Database
        /// </summary>
        /// <param name="query">The query to search for the model</param>
        /// <param name="set">The set to remove the model from</param>
        /// <param name="filter">The filter used to target the model</param>
        /// <param name="save">A boolean indicating if the change should be committed to the DB</param>
        /// <typeparam name="TInput">The type of the model</typeparam>
        /// <returns>The removed model</returns>
        protected async Task<TInput> DeleteModel<TInput>(
            IQueryable<TInput> query,
            DbSet<TInput> set,
            Expression<Func<TInput, bool>> filter,
            bool save = true
        ) where TInput : class {
            var model = await GetModel(query, filter);
            set.Remove(model);
            if (save) await _context.SaveChangesAsync();
            return model;
        }

        /// <summary>
        /// Deletes a model from the Database
        /// </summary>
        /// <param name="set">The set to search and remove the model from</param>
        /// <param name="filter">The filter used to target the model</param>
        /// <param name="save">A boolean indicating if the change should be committed to the DB</param>
        /// <typeparam name="TInput">The type of the model</typeparam>
        /// <returns>The removed model</returns>
        protected Task<TInput> DeleteModel<TInput>(
            DbSet<TInput> set,
            Expression<Func<TInput, bool>> filter,
            bool save = true
        )
            where TInput : class {
            return DeleteModel(set, set, filter, save);
        }
        
        private NotFoundException GenerateError<TInput>(Expression<Func<TInput, bool>> filter = null) {
	        if (filter == null || filter.Body.NodeType != ExpressionType.Equal)
		        return NotFoundException.NotExists<TInput>();

	        var op = (BinaryExpression) filter.Body;

	        string param = null;
	        string side = null;
	        if (op.Left.NodeType == ExpressionType.MemberAccess) {
		        var left = (MemberExpression) op.Left;
		        if (left.Member.DeclaringType == typeof(TInput)) {
			        param = left.Member.Name;
			        side = "left";
		        }
	        }

	        if (param == null && op.Right.NodeType == ExpressionType.MemberAccess) {
		        var right = (MemberExpression) op.Right;
		        if (right.Member.DeclaringType == typeof(TInput)) {
			        param = right.Member.Name;
			        side = "right";
		        }
	        }

	        if (param == null)
		        return NotFoundException.NotExists<TInput>();

	        var value = Expression
		        .Lambda(side == "left" ? (MemberExpression) op.Right : (MemberExpression) op.Left)
		        .Compile()
		        .DynamicInvoke();

	        return NotFoundException.NotExistsWithProperty<TInput>(param, value.ToString());
        }
    }
}
