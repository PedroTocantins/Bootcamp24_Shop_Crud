import { Component, OnInit } from '@angular/core';
import { Category } from '../../Models/Category/Category';
import { CategoryRequest } from '../../Models/Category/CategoryRequest';
import { ToastrService } from 'ngx-toastr';
import { CategoryService } from '../../services/Category/category.service';
import {
  FormControl,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-category-list',
  standalone: true,
  imports: [ReactiveFormsModule],
  templateUrl: './category-list.component.html',
  styleUrl: './category-list.component.css',
})
export class CategoryListComponent implements OnInit {
  categoryForm = new FormGroup({
    name: new FormControl('', Validators.required),
    description: new FormControl(''),
  });

  categories?: Category[];
  currentId?: string;

  constructor(
    private categoryService: CategoryService,
    private toastr: ToastrService
  ) {}
  ngOnInit(): void {
    this.getAllCategories();
  }

  getCategoryValue(category: Category) {
    this.categoryForm.patchValue({
      name: category.name ?? 'Default',
      description: category.description ?? 'Default',
    });
    this.currentId = category.id;
  }

  choosePutOrPost(category: CategoryRequest) {
    if (this.currentId) {
      this.updateCategory(this.currentId, category);
    } else {
      this.addCategory(category);
    }
  }

  getAllCategories() {
    this.categoryService.getAll().subscribe({
      next: (response: Category[]) => {
        this.categories = response;
      },
      error: () => {
        this.toastr.error('An error occurred while fetching categories');
      },
    });
  }

  onSubmit() {
    const newCategory = new CategoryRequest(
      this.categoryForm.value.name ?? 'Default',
      this.categoryForm.value.description ?? 'Default'
    );

    this.choosePutOrPost(newCategory);
  }

  addCategory(category: CategoryRequest) {
    this.categoryService.addCategory(category).subscribe({
      next: () => {
        this.toastr.success('Success');
        this.getAllCategories();
        this.resetForm();
      },
      error: () => {
        this.toastr.error('An error occurred');
      },
    });
  }

  updateCategory(id: string, category: CategoryRequest) {
    this.categoryService.updateCategory(id, category).subscribe({
      next: () => {
        this.toastr.success('Success');
        this.getAllCategories();
        this.resetForm();
      },
      error: () => {
        this.toastr.error('An error occurred');
      },
    });
  }

  deleteCategory() {
    this.categoryService.deleteCategory(this.currentId ?? '').subscribe({
      next: () => {
        this.toastr.success('Success');
        this.getAllCategories();
        this.resetForm();
      },
      error: () => {
        this.toastr.error('An error occurred');
      },
    });
  }

  resetForm() {
    this.currentId = undefined;
    this.categoryForm.reset();
  }

  trackByCategoryId(index: number, category: Category): string {
    return category.id;
  }
}
