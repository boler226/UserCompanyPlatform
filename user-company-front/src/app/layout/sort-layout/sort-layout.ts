import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-sort-layout',
  imports: [
    FormsModule
  ],
  templateUrl: './sort-layout.html',
  standalone: true,
  styleUrl: './sort-layout.scss'
})
export class SortLayoutComponent {
  @Input() sortOptions: { label: string, value: string }[] = [];
  @Output() sort = new EventEmitter<string>();
  selectedSort: string | null = null;

  onSortChange(value: string) {
    if (this.selectedSort !== value) {
      this.selectedSort = value;
      this.sort.emit(value);
    }
  }

  onSortReset() {
    this.selectedSort = null;
    this.sort.emit("none");
  }
}
