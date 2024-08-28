import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { Medico } from '../../interfaces/medico';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import { MedicoService } from '../../servicios/medico.service';
import { CompartidoService } from 'src/app/compartido/compartido.service';
import { MatDialog } from '@angular/material/dialog';
import { ModalMedicoComponent } from '../../modales/modal-medico/modal-medico.component';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-listado-medico',
  templateUrl: './listado-medico.component.html',
  styleUrls: ['./listado-medico.component.css']
})
export class ListadoMedicoComponent implements OnInit,AfterViewInit
 {

  displayedColumns :string[]=[
    'Apellidos',
    'Nombres',
    'Telefono',
    'Genero',
    'Estado',
    'acciones'
  ];
  dataIncial: Medico[]=[];
  dataSource = new MatTableDataSource(this.dataIncial);
  @ViewChild(MatPaginator) paginator !: MatPaginator;
  constructor(
    private _medicoServicio:MedicoService,
    private _compartido:CompartidoService,
    private dialog :MatDialog
  ){

  }
  obtenerMedicos(){
    this._medicoServicio.lista().subscribe({
      next:(data)=>{
        if(data.isExitoso){
          this.dataSource=new MatTableDataSource(data.resultado);
          this.dataSource.paginator =this.paginator;
        }else{
          this._compartido.mostrarAlerta('no se encontraron datos','Advertencia')
        }
      },
      error:(e)=>{

        this._compartido.mostrarAlerta(e.error.mensaje,'Advertencia!')

      }
    })
  }

  nuevoMedico(){
    this.dialog
        .open(ModalMedicoComponent,{disableClose:true, width:'600px'})
        .afterClosed()
        .subscribe((resltado)=>{
          if(resltado==true){
            this.obtenerMedicos();
          }
        })
  }

  editarMedico(medico:Medico){
    this.dialog
      .open(ModalMedicoComponent,{disableClose:true,width:'600px',data:medico})
      .afterClosed()
      .subscribe((resultado)=>{
        if(resultado==true){
          this.obtenerMedicos();
        }
      })
  }
  removerMedico(medico:Medico){
    Swal.fire({
      title:'Desea elminar el medico',
      text:medico.Apellidos+' '+medico.Nombres,
      icon:'warning',
      confirmButtonColor:'#3085d',
      confirmButtonText:'Si,Eliminar',
      showCancelButton:true,
      cancelButtonColor:'#d33',
      cancelButtonText:'No',
      
    }).then((resultado)=>{
      if(resultado.isConfirmed){
        this._medicoServicio.eliminar(medico.Id).subscribe({
          next:(data)=>{
            if(data.isExitoso){
              this._compartido.mostrarAlerta("el medico fue eliminado",'complete');
              this.obtenerMedicos();
            }else{
              this._compartido.mostrarAlerta("No se puedo eliminar el medico",'error');

            }
          },
          error:(e)=>{
            this._compartido.mostrarAlerta(e.error.mensaje,'Advertencia!')

          }
        });
      }
    })
  }
  aplicarFiltroListado(event:Event){
    const filterValue=(event.target as HTMLInputElement).value;
    this.dataSource.filter=filterValue.trim().toLowerCase();
    if(this.dataSource.paginator){
      this.dataSource.paginator=this.paginator
    }
  }
  ngAfterViewInit(): void {
    this.dataSource.paginator = this.paginator;
  }
  ngOnInit(): void {
    this.obtenerMedicos();

  }

}
