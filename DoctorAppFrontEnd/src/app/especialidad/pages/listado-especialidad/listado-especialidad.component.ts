import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Especialidad } from '../../interfaces/especialidad';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { EspecialidadService } from '../../servicios/especialidad.service';
import { CompartidoService } from 'src/app/compartido/compartido.service';

@Component({
  selector: 'app-listado-especialidad',
  templateUrl: './listado-especialidad.component.html',
  styleUrls: ['./listado-especialidad.component.css']
})
export class ListadoEspecialidadComponent implements OnInit, AfterViewInit  {

  displayedColumns :string[]=[
    'nombreEspecialidad',
    'descripcion',
    'estado',
    'acciones'
    ]
  dataIncial:Especialidad[]=[];
  dataSource = new MatTableDataSource(this.dataIncial);
  @ViewChild(MatPaginator)paginacionTabla!:MatPaginator;
  constructor(private _especialidadServicio:EspecialidadService,private _compartidadoService:CompartidoService){}

  obtenerEspecialidades(){
    this._especialidadServicio.lista().subscribe({
      next:(data)=>{
        if(data.isExitoso){
          this.dataSource=new MatTableDataSource(data.resultado)
          this.dataSource.paginator = this.paginacionTabla;
        }else{
          this._compartidadoService.mostrarAlerta('no se encontraron datos','Advertencia!')
        }
      },
      error:(e)=>{}
    })
  }
  ngAfterViewInit(): void {
    this,this.dataSource.paginator=this.paginacionTabla;
  }
  ngOnInit():void{
    this.obtenerEspecialidades();


  }



}
