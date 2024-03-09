import { Component, OnInit } from '@angular/core';
import { TarefaService } from 'src/app/services/tarefa-service.service';
import { Tarefa } from 'src/app/models/Tarefas';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-detalhes',
  templateUrl: './detalhes.component.html',
  styleUrls: ['./detalhes.component.css']
})
export class DetalhesComponent implements OnInit{

   tarefa?: Tarefa;
   id!:number;

  constructor(private tarefaService: TarefaService, private route: ActivatedRoute, private router : Router) {

  }

  ngOnInit(): void {

      this.id =  Number(this.route.snapshot.paramMap.get("id"));

      this.tarefaService.GetTarefa( this.id).subscribe((data) => {
         const dados = data.result;
         dados.data = new Date(dados.data!).toLocaleDateString("pt-BR");
         this.tarefa = dados;
      });
  }


  InativaTarefa(){

      this.tarefaService.InativaTarefa(this.id).subscribe((data) => {
        this.router.navigate(['']);
        }
      );

  }
}
