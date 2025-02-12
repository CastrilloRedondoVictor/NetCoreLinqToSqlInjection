using Microsoft.AspNetCore.Mvc;
using NetCoreLinqToSqlinjection.Repositories;
using NetCoreLinqToSqlInjection.Models;

public class DoctoresController : Controller
{
    IRepositoryDoctores repo;

    public DoctoresController(IRepositoryDoctores repo)
    {
        this.repo = repo;
    }

    public IActionResult Index()
    {
        List<Doctor> doctores = this.repo.GetDoctores();
        return View(doctores);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(Doctor doc)
    {
        this.repo.InsertarDoctor(doc.DoctorNo, doc.Apellido
        , doc.Especialidad, doc.Salario, doc.HospitalCod);
        return RedirectToAction("Index");
    }

    public IActionResult Delete(int iddoctor)
    {
        this.repo.DeleteDoctor(iddoctor);
        return RedirectToAction("Index");
    }
    public IActionResult Update(int iddoctor)
    {
        Doctor doctor = this.repo.FindDoctor(iddoctor);
        return View(doctor);
    }

    [HttpPost]
    public IActionResult Update(Doctor doctor)
    {
        this.repo.UpdateDoctor(doctor.HospitalCod, doctor.DoctorNo, doctor.Apellido, doctor.Especialidad, doctor.Salario);
        return RedirectToAction("Index");
    }

    public IActionResult Filter()
    {
        List<Doctor> doctores = this.repo.GetDoctores();
        ViewData["DOCTORES"] = doctores;
        return View();
    }

    [HttpPost]
    public IActionResult Filter(string especialidad)
    {
        List<Doctor> doctoresFilter =  this.repo.FindDoctoresEspecialidad(especialidad);

        List<Doctor> doctores = this.repo.GetDoctores();
        ViewData["DOCTORES"] = doctores;

        return View(doctoresFilter);
    }

}