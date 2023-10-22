﻿// <auto-generated />
using System;
using ControleFinanceiro.Dados.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ControleFinanceiro.Dados.Migrations
{
    [DbContext(typeof(ControleFinanceiroDbContext))]
    partial class ControleFinanceiroDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ControleFinanceiro.Dominio.Entities.Categorias", b =>
                {
                    b.Property<int>("CategoriaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("CategoriaId"));

                    b.Property<string>("CategoriaDescricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CategoriaNome")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("CategoriaId");

                    b.ToTable("Categorias");
                });

            modelBuilder.Entity("ControleFinanceiro.Dominio.Entities.Despesas", b =>
                {
                    b.Property<int>("DespesaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DespesaId"));

                    b.Property<int>("CategoriaId")
                        .HasColumnType("int");

                    b.Property<string>("DespesaDescricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("DespesaName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("DespesaValor")
                        .HasColumnType("float");

                    b.Property<DateTime>("DespesasData")
                        .HasColumnType("datetime2");

                    b.Property<int>("DespesasQuantidadeMeses")
                        .HasColumnType("int");

                    b.Property<int>("StatusDespesas")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("DespesaId");

                    b.HasIndex("CategoriaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Despesas");
                });

            modelBuilder.Entity("ControleFinanceiro.Dominio.Entities.Receitas", b =>
                {
                    b.Property<int>("ReceitaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReceitaId"));

                    b.Property<DateTime>("ReceitaData")
                        .HasColumnType("datetime2");

                    b.Property<string>("ReceitaDescricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("ReceitaName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<int>("ReceitaQuantidadeMeses")
                        .HasColumnType("int");

                    b.Property<double>("ReceitaValor")
                        .HasColumnType("float");

                    b.Property<int>("TipoValor")
                        .HasColumnType("int");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("ReceitaId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Receitas");
                });

            modelBuilder.Entity("ControleFinanceiro.Dominio.Entities.Usuarios", b =>
                {
                    b.Property<int>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UsuarioId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ControleFinanceiro.Dominio.Entities.Despesas", b =>
                {
                    b.HasOne("ControleFinanceiro.Dominio.Entities.Categorias", "Categorias")
                        .WithMany("Despesas")
                        .HasForeignKey("CategoriaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControleFinanceiro.Dominio.Entities.Usuarios", "Usuarios")
                        .WithMany("Despesas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Categorias");

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("ControleFinanceiro.Dominio.Entities.Receitas", b =>
                {
                    b.HasOne("ControleFinanceiro.Dominio.Entities.Usuarios", "Usuarios")
                        .WithMany("Receitas")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("ControleFinanceiro.Dominio.Entities.Categorias", b =>
                {
                    b.Navigation("Despesas");
                });

            modelBuilder.Entity("ControleFinanceiro.Dominio.Entities.Usuarios", b =>
                {
                    b.Navigation("Despesas");

                    b.Navigation("Receitas");
                });
#pragma warning restore 612, 618
        }
    }
}
