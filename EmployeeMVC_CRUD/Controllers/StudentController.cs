using EmployeeMVC_CRUD.DAL;
using EmployeeMVC_CRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeMVC_CRUD.Controllers
{
    public class StudentController : Controller
    {
        // Displays a list of all students
        public ActionResult Index()
        {
            List<Student> students = EmpDAL.GetAllStudents();
            return View("stuindex", students);
        }

        // GET: Displays the form to create a new student
        public ActionResult Create()
        {
            return View();
        }

        // POST: Handles the submission of the new student form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                EmpDAL.InsertStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Displays the form to edit an existing student
        public ActionResult Edit(int id)
        {
            // Retrieve the student from the database based on the provided id
            Student student = EmpDAL.GetStudentById(id);
            if (student == null)
            {
                return NotFound(); // Return a 404 if student not found
            }
            return View(student);
        }

        // POST: Handles the submission of the edited student form
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                EmpDAL.UpdateStudent(student);
                return RedirectToAction("Index");
            }
            return View(student);
        }

        // GET: Displays a confirmation page before deleting a student
        public ActionResult Delete(int id)
        {
            Student student = EmpDAL.GetStudentById(id);
            if (student == null)
            {
                return NotFound(); // Return a 404 if student not found
            }
            return View(student);
        }

        // POST: Handles the deletion of a student after confirmation
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmpDAL.DeleteStudent(id);
            return RedirectToAction("Index");
        }
    }
}