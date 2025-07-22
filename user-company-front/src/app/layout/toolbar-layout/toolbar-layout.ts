import {Component, EventEmitter, Input, Output} from '@angular/core';
import {FormsModule} from '@angular/forms';

@Component({
  selector: 'app-toolbar-layout',
  imports: [
    FormsModule
  ],
  templateUrl: './toolbar-layout.html',
  standalone: true,
  styleUrl: './toolbar-layout.scss'
})
export class ToolbarLayoutComponent {
  @Input() searchEnabled: boolean = true;
  @Output() search = new EventEmitter<string>();

  searchText: string = '';

  onSearchChange() {
    this.search.emit(this.searchText);
  }
}
