import { Component, OnInit, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, ActivatedRoute } from '@angular/router';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { EmpreendimentoService } from '../../../core/services/empreendimento';

@Component({
  selector: 'app-empreendimento-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './empreendimento-form.html',
  styleUrl: './empreendimento-form.css',
})
export class EmpreendimentoForm implements OnInit {
  form!: FormGroup;
  modoEdicao = false;
  empreendimentoId: string | null = null;

  carregando = false;
  salvando = false;
  erro = '';

  constructor(
    private fb: FormBuilder,
    private empreendimentoService: EmpreendimentoService,
    private router: Router,
    private route: ActivatedRoute,
    private cdr: ChangeDetectorRef,
  ) {}

  ngOnInit(): void {
    this.empreendimentoId = this.route.snapshot.paramMap.get('id');
    this.modoEdicao = !!this.empreendimentoId;

    this.form = this.fb.group({
      nome: ['', [Validators.required, Validators.minLength(3)]],
      cnpj: ['', [Validators.required]],
      endereco: [''],
    });

    if (this.modoEdicao) {
      this.carregarDados();
    }
  }

  carregarDados(): void {
    this.carregando = true;

    this.empreendimentoService.getById(this.empreendimentoId!).subscribe({
      next: (item) => {
        this.form.patchValue({
          nome: item.nome,
          cnpj: item.cnpj,
          endereco: item.endereco,
        });
        // No modo edição, CNPJ não pode ser alterado
        this.form.get('cnpj')?.disable();

        this.carregando = false;
        this.cdr.detectChanges();
      },
      error: () => {
        this.erro = 'Empreendimento não encontrado.';
        this.carregando = false;
        this.cdr.detectChanges();
      },
    });
  }

  salvar(): void {
    if (this.form.invalid) {
      this.form.markAllAsTouched();
      return;
    }

    this.salvando = true;
    this.erro = '';

    if (this.modoEdicao && this.empreendimentoId) {
      const payload = {
        nome: this.form.value.nome,
        endereco: this.form.value.endereco,
      };

      this.empreendimentoService.update(this.empreendimentoId, payload).subscribe({
        next: () => {
          this.salvando = false;
          this.cdr.detectChanges();
          this.router.navigate(['/empreendimentos'], {
            queryParams: { sucesso: 'editado' },
          });
        },
        error: (err) => {
          this.erro = err?.error?.mensagem || 'Erro ao atualizar empreendimento.';
          this.salvando = false;
          this.cdr.detectChanges();
        },
      });
    } else {
      const payload = {
        nome: this.form.value.nome,
        cnpj: this.form.value.cnpj,
        endereco: this.form.value.endereco,
      };

      this.empreendimentoService.create(payload).subscribe({
        next: () => {
          this.salvando = false;
          this.cdr.detectChanges();
          this.router.navigate(['/empreendimentos'], {
            queryParams: { sucesso: 'criado' },
          });
        },
        error: (err) => {
          this.erro = err?.error?.mensagem || 'Erro ao cadastrar empreendimento.';
          this.salvando = false;
          this.cdr.detectChanges();
        },
      });
    }
  }

  cancelar(): void {
    this.router.navigate(['/empreendimentos']);
  }
}
