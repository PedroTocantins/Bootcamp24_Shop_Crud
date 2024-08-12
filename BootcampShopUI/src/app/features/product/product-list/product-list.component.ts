import { ProductRequest } from './../../Models/ProductRequest';
import { Product } from './../../Models/Product';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ProductService } from '../../services/product.service';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ToastrService } from 'ngx-toastr';
import { error } from 'console';
import { CategoryService } from '../../services/Category/category.service';
import { Category } from '../../Models/Category/Category';

@Component({
  selector: 'app-product-list',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css',
})
export class ProductListComponent implements OnInit {
  productForm = new FormGroup({
    name: new FormControl('', Validators.required),
    description: new FormControl(''),
    price: new FormControl(0, [Validators.required, Validators.min(0)]),
    stock: new FormControl(0, [Validators.required, Validators.min(0)]),
    categoryId: new FormControl(''),
  });

  products?: Product[];
  categories?: Category[];

  currentId?: string;
  constructor(
    private productService: ProductService,
    private categoryService: CategoryService,
    private toastr: ToastrService
  ) {}

  ngOnInit(): void {
    this.getAll();
  }

  getProducValue(product: Product) {
    this.productForm.patchValue({
      name: product.name ?? 'Default',
      description: product.description ?? 'Default',
      price: product.price ?? 0,
      stock: product.stock ?? 0,
      categoryId: product.categoryId,
    });
    this.currentId = product.id;
  }

  choosePutOrPost(product: ProductRequest) {
    if (this.currentId) {
      this.updateProduct(this.currentId, product);
    } else {
      this.addProduct(product);
    }
  }

  getAll() {
    this.productService.getAll().subscribe({
      next: (response: Product[]) => {
        this.products = response;
      },
    });
  }

  getAllCategories() {
    this.categoryService.getAll().subscribe({
      next: (response: Category[]) => {
        this.categories = response;
      },
    });
  }

  onSubmit() {
    const newProduct = new ProductRequest(
      this.productForm.value.name ?? 'Default',
      this.productForm.value.description ?? 'Default',
      this.productForm.value.price ?? 0,
      this.productForm.value.stock ?? 0,
      this.productForm.value.categoryId ?? 'Default'
    );

    this.choosePutOrPost(newProduct);
  }

  addProduct(product: ProductRequest) {
    console.log(product);
    this.productService.addProduct(product).subscribe({
      next: (response) => {
        this.toastr.success('Success');
        this.getAll();
        this.resetForm();
      },
      error: (error) => {
        console.log(error);
        this.toastr.error('An error ocur');
      },
    });
  }

  updateProduct(id: string, product: ProductRequest) {
    this.productService.updateProduct(id, product).subscribe({
      next: (response) => {
        this.toastr.success('Success');
        this.getAll();
        this.resetForm();
      },
      error: (error) => {
        this.toastr.error('An error ocur');
      },
    });
  }

  deleteProduct() {
    this.productService.deleteProduct(this.currentId ?? '').subscribe({
      next: (response) => {
        this.toastr.success('Success');
        this.getAll();
        this.resetForm();
      },
      error: (error) => {
        this.toastr.error('An error ocur');
      },
    });
  }

  resetForm() {
    this.getAllCategories();
    this.currentId = undefined;
    this.productForm.reset();
  }
}
