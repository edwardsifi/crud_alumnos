using Crud_alumnos.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Crud_alumnos.Controllers
{
    public class AlumnosController : Controller
    {
        // GET: Alumnos
        public ActionResult Index()
        {
            try
            {
                //int edad = 18;

                //string sql = @"select a.Id, a.Nombres, a.Apellidos, a.Edad, a.Sexo, a.FechaRegistro, c.Nombre as NombreCiudad
                //           from Alumno a inner join Ciudad c on a.CodCiudad = c.Id where a.Edad > @edadAlumno";

                using (var db = new AlumnosContext())
                {



                    //usando linq para mapar alumnoce y obtener sus datos
                     var data = from a in db.Alumno
                                join c in db.Ciudad on a.CodCiudad  equals c.id
                                select new AlumnoCE() {

                                    Id = a.Id,
                                    Nombres = a.Nombres,
                                    Apellidos = a.Apellidos,
                                    Edad = a.Edad,
                                    Sexo = a.Sexo,
                                    NombreCiudad = c.Nombre,
                                    FechaRegistro = a.FechaRegistro

                                };

                     //usando linkiu de identity framework con expreciones lamda
                     List<Alumno> lista = db.Alumno.Where(a => a.Edad > 18).ToList();
                     //return View(db.Alumno.ToList());
                     return View(data.ToList());


                    //return View(db.Database.SqlQuery<AlumnoCE>(sql).ToList());

                    //return View(db.Database.SqlQuery<AlumnoCE>(sql, new SqlParameter("@edadAlumno", edad)).ToList());
                }
            }
            catch (Exception)
            {

                throw;
            }


            //conexion
            //AlumnosContext db = new AlumnosContext();

            //usar linkiu de identiti framework
            //db.Alumno.ToList();
            //crea una lista con los alumnos mayores a 18
            //List<Alumno> lista = db.Alumno.Where(a => a.Edad > 18).ToList(); 

            // return View(db.Alumno.ToList()); //envia todos los alumnos como una lista
            //return View(db.Alumno.ToList)
        }

        public ActionResult Agregar()
        {

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Agregar(Alumno a)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }


            try
            {

                using (var db = new AlumnosContext())
                {
                    a.FechaRegistro = DateTime.Now;
                    db.Alumno.Add(a);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError(" ", "error al agregar el alumno - " + ex.Message);
                return View();
            }


        }


        public ActionResult Agregar2() {
            return View();
        }


        public ActionResult ListaCiudades() {

            using (var db = new AlumnosContext()) {

                return PartialView(db.Ciudad.ToList());
            }
        
        }

        public ActionResult Editar(int id)
        {

            

            try
            {
                using (var db = new AlumnosContext())
                {

                    //Alumno al = db.Alumno.Where(a => a.Id == id).FirstOrDefault(); funciona en todos los casos
                    Alumno alu = db.Alumno.Find(id);// no funciona con dos claves primarias iguales
                    return View(alu);
                }
            }
            catch (Exception ex)
            {

                throw;
            }




        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(Alumno a)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                using (var db = new AlumnosContext())
                {

                    Alumno al = db.Alumno.Find(a.Id);
                    al.Nombres = a.Nombres;
                    al.Apellidos = a.Apellidos;
                    al.Edad = a.Edad;
                    al.Sexo = a.Sexo;
                    

                    db.SaveChanges();

                    return RedirectToAction("Index");

                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public ActionResult Detallesalumno(int id)
        {

            using (var db = new AlumnosContext())
            {
                Alumno alu = db.Alumno.Find(id);
                return View(alu);
            }

        }


        public ActionResult EliminarAlumno(int id)
        {

            using (var db = new AlumnosContext())
            {
                Alumno alu = db.Alumno.Find(id);
                db.Alumno.Remove(alu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }


        public static string NombreCiudad(int CodCiudad) {
            using (var db = new AlumnosContext()) {
                return db.Ciudad.Find(CodCiudad).Nombre;
            }
        }




    }
}