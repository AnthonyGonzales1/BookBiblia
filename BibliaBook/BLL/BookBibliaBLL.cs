using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BibliaBook.DAL;
using BibliaBook.Entidades;

namespace BibliaBook.BLL
{
    public class BibliaBookBLL
    {
        public static bool Guardar(Biblia book)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Biblias.Add(book) != null)
                {
                    contexto.SaveChanges();
                    contexto.Dispose();
                    paso = true;
                }
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public static bool Eliminar(int Id)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                var eliminar = contexto.Biblias.Find(Id);
                if (eliminar != null)
                {
                    contexto.Entry(eliminar).State = EntityState.Deleted;
                    if (contexto.SaveChanges() > 0)
                    {
                        contexto.Dispose();
                        paso = true;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public static bool Modificar(Biblia book)
        {
            bool paso = false;
            Contexto contexto = new Contexto();
            try
            {
                contexto.Entry(book).State = EntityState.Modified;
                if (contexto.SaveChanges() > 0)
                {
                    contexto.Dispose();
                    paso = true;
                }
            }
            catch (Exception)
            {
                throw;
            }

            return paso;
        }

        public static Biblia Buscar(int id)
        {
            Biblia book = new Biblia();
            Contexto contexto = new Contexto();
            try
            {

                book = contexto.Biblias.Find(id);
                contexto.Dispose();
            }
            catch (Exception)
            {

                throw;
            }
            return book;
        }
        
        public static List<Biblia> GetList(Expression<Func<Biblia, bool>> booki)
        {
            List<Biblia> books = new List<Biblia>();
            Contexto contexto = new Contexto();
            try
            {
                books = contexto.Biblias.Where(booki).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return books;
        }
    }
}
