using Libreria.DataAccessLayer.DataContext;
using Libreria.DataAccessLayer.Repositories.Contract;
using Libreria.Models;
using Microsoft.EntityFrameworkCore;

namespace Libreria.DataAccessLayer.Repositories;

public class ProductoRepository : IProductoRepository
{
    private readonly LibreriaContext _context;
    public ProductoRepository(LibreriaContext context)
    {
        _context = context;
    }

    public async Task<Producto> AddAsync(Producto entity)
    {
        try
        {
            await _context.Productos.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al agregar el producto: {ex.Message}");
        }
    }

    public async Task<Producto> DeleteAsync(int id)
    {
        try
        {
            var productoToDelete = await _context.Productos.FindAsync(id);
            if (productoToDelete != null)
            {
                _context.Productos.Remove(productoToDelete);
                await _context.SaveChangesAsync();
                return productoToDelete;
            }
            throw new Exception("Producto no encontrado");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al eliminar el producto: {ex.Message}");
        }
    }

    public async Task<List<TResult>> ExecuteQueryAsync<TResult>(string query) where TResult : class
    {
        try
        {
            return await _context.Set<TResult>().FromSqlRaw(query).ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al ejecutar la consulta: {ex.Message}");
        }
    }

    public Task<IQueryable<Producto>> GetAllAsync()
    {
        try
        {
            IQueryable<Producto> productos = _context.Productos;
            return Task.FromResult(productos);
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener los productos: {ex.Message}");
        }
    }

    public async Task<Producto> GetByIdAsync(int id)
    {
        try
        {
            var productoToDatabase = await _context.Productos.FindAsync(id);
            if (productoToDatabase != null)
            {
                return productoToDatabase;
            }
            throw new Exception("Producto no encontrado");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al obtener el producto: {ex.Message}");
        }
    }

    public async Task<List<Producto>> SearchProductsByNameAsync(string textSearch)
    {
        try
        {
            return await _context.Productos
                    .Where(p => EF.Functions.Like(p.NombreProducto, $"%{textSearch}%"))
                    .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al buscar los productos por nombre: {ex.Message}");
        }
    }

    public async Task<Producto> UpdateAsync(Producto entity)
    {
        try
        {
            var productoToDatabase = await _context.Productos.FindAsync(entity.Id);
            if (productoToDatabase != null)
            {
                _context.Update(entity);
                await _context.SaveChangesAsync();
                return productoToDatabase;
            }
            throw new Exception("Producto no encontrado");
        }
        catch (Exception ex)
        {
            throw new Exception($"Error al actualizar el producto: {ex.Message}");
        }
    }
}
