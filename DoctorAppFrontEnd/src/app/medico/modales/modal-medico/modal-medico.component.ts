import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA, MatDialog, MatDialogRef } from '@angular/material/dialog';
import {Especialidad} from 'src/app/especialidad/interfaces/especialidad'
import { Medico } from '../../interfaces/medico';
import { EspecialidadService } from 'src/app/especialidad/servicios/especialidad.service';
import { MedicoService } from '../../servicios/medico.service';
import { CompartidoService } from 'src/app/compartido/compartido.service';
@Component({
  selector: 'app-modal-medico',
  templateUrl: './modal-medico.component.html',
  styleUrls: ['./modal-medico.component.css']
})
export class ModalMedicoComponent implements OnInit {


  formMedico : FormGroup;
  titulo : string ='Agregar';
  nombreBoton:string ="Guardar";
  listaEspecialidades : Especialidad[] =[];

  constructor(private modal:MatDialogRef<ModalMedicoComponent>,
    @Inject(MAT_DIALOG_DATA)public datosMedico:Medico, private fb:FormBuilder,private _especialidadServicio : EspecialidadService, private _medicoServicio :MedicoService
    ,private _compartido:CompartidoService){
      this.formMedico = this.fb.group({
        Apellidos: ['',Validators.required],
        
        Nombres: ['',Validators.required],
        
        Direccion: ['',Validators.required],
        Telefono: [''],
        Genero: ['M',Validators.required],
        EspecialidadId: ['',Validators.required],
        Estado: ['1',Validators.required],




      })
      if(this.datosMedico !=null){
        this.titulo='Editar';
        this.nombreBoton='Actualizar';
      }
      this._especialidadServicio.listaActivos().subscribe({
        next:(data)=>{
          if(data.isExitoso){
            this.listaEspecialidades = data.resultado;
          }
        },
        error:(e)=>{}
      })

  }
  ngOnInit(): void {
    if(this.datosMedico !=null){
      this.formMedico.patchValue({
        Apellidos : this.datosMedico.Apellidos,
        Nombres : this.datosMedico.Nombres,
        Direccion:this.datosMedico.Direccion,
        Telefono:this.datosMedico.Telefono,
        Genero:this.datosMedico.Genero,
        EspecialidadId:this.datosMedico.EspecialidadId,
        Estado:this.datosMedico.Estado.toString()
      })
    }
  }
  crearModificarMedico(){
    const medico :Medico={
      Id:this.datosMedico == null ?0 : this.datosMedico.Id,
      Apellidos:this.formMedico.value.Apellidos,
      Nombres:this.formMedico.value.Nombres,
      Direccion:this.formMedico.value.Direccion,
      Telefono:this.formMedico.value.Telefono,
      Genero:this.formMedico.value.Genero,
      EspecialidadId:parseInt(this.formMedico.value.EspecialidadId),
      Estado: parseInt(this.formMedico.value.Estado),
      NombreEspecialidad:''
    }
    if(this.datosMedico ==null){
      this._medicoServicio.crear(medico).subscribe({
        next:(data)=>{
          if(data.isExitoso){
            this._compartido.mostrarAlerta('el medico se creo con exito','Completado')
            this.modal.close("true");
          }else{
            this._compartido.mostrarAlerta("no se pudo crear el medico",'Error')
          }

          
        },
        error:(e)=>{
          this._compartido.mostrarAlerta(e.error.mensaje ,'Error')

        }
      })
    }else{
      this._medicoServicio.editar(medico).subscribe({
        next:(data)=>{
          if(data.isExitoso){
            this._compartido.mostrarAlerta('el medico se actualizo con exito','Completado')
            this.modal.close("true");
          }else{
            this._compartido.mostrarAlerta("no se pudo actualizar el medico",'Error')
          }

          
        },
        error:(e)=>{
          this._compartido.mostrarAlerta(e.error.mensaje ,'Error')

        }
      })
    }
  }
}
