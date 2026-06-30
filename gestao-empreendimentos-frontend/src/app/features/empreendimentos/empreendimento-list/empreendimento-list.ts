import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink, ActivatedRoute } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { Subject } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { EmpreendimentoService } from '../../../core/services/empreendimento';
import { EmpreendimentoResponse } from '../../../core/models/empreendimento';

@Component({
  selector: 'app-empreendimento-list',
  standalone: true,
  imports: [CommonModule, RouterLink, FormsModule],
  templateUrl: './empreendimento-list.html',
  styleUrl: './empreendimento-list.css',
})
export class EmpreendimentoList implements OnInit {
  empreendimentos: EmpreendimentoResponse[] = [];
  carregando = false;
  erro = '';
  sucesso = '';

  filtroNome = '';
  filtroStatus = '';
  ordenarPor = 'nome';

  // Paginação
  paginaAtual = 1;
  tamanhoPagina = 5;
  totalRegistros = 0;
  totalPaginas = 0;

  private buscar$ = new Subject<void>();

  constructor(
    private empreendimentoService: EmpreendimentoService,
    private cdr: ChangeDetectorRef,
    private route: ActivatedRoute,
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      if (params['sucesso'] === 'criado') {
        this.sucesso = 'Empreendimento cadastrado com sucesso!';
      } else if (params['sucesso'] === 'editado') {
        this.sucesso = 'Empreendimento atualizado com sucesso!';
      }

      if (this.sucesso) {
        setTimeout(() => {
          this.sucesso = '';
          this.cdr.detectChanges();
        }, 3000);
      }
    });

    this.buscar$
      .pipe(
        switchMap(() => {
          this.carregando = true;
          this.erro = '';
          return this.empreendimentoService.getAll(
            this.filtroNome,
            this.filtroStatus,
            this.ordenarPor,
            this.paginaAtual,
            this.tamanhoPagina,
          );
        }),
      )
      .subscribe({
        next: (resultado) => {
          this.empreendimentos = resultado.itens;
          this.totalRegistros = resultado.totalRegistros;
          this.totalPaginas = resultado.totalPaginas;
          this.carregando = false;
          this.cdr.detectChanges();
        },
        error: () => {
          this.erro = 'Erro ao carregar empreendimentos. Tente novamente.';
          this.carregando = false;
          this.cdr.detectChanges();
        },
      });

    this.carregar();
  }

  carregar(): void {
    this.buscar$.next();
  }

  aplicarFiltros(): void {
    this.paginaAtual = 1; // sempre volta para página 1 ao mudar filtros
    this.carregar();
  }

  irParaPagina(pagina: number): void {
    if (pagina < 1 || pagina > this.totalPaginas) return;
    this.paginaAtual = pagina;
    this.carregar();
  }

  paginaAnterior(): void {
    this.irParaPagina(this.paginaAtual - 1);
  }

  proximaPagina(): void {
    this.irParaPagina(this.paginaAtual + 1);
  }

  inativar(id: string): void {
    if (!confirm('Deseja realmente inativar este empreendimento?')) {
      return;
    }

    this.empreendimentoService.inativar(id).subscribe({
      next: () => {
        this.sucesso = 'Empreendimento inativado com sucesso.';
        this.cdr.detectChanges();
        this.carregar();
        setTimeout(() => {
          this.sucesso = '';
          this.cdr.detectChanges();
        }, 3000);
      },
      error: () => {
        this.erro = 'Erro ao inativar empreendimento.';
        this.cdr.detectChanges();
        setTimeout(() => {
          this.erro = '';
          this.cdr.detectChanges();
        }, 3000);
      },
    });
  }
}
