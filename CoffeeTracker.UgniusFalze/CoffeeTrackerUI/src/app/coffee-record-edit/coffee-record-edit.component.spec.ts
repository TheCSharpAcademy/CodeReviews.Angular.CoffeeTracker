import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoffeeRecordEditComponent } from './coffee-record-edit.component';

describe('CoffeeRecordEditComponent', () => {
  let component: CoffeeRecordEditComponent;
  let fixture: ComponentFixture<CoffeeRecordEditComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CoffeeRecordEditComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CoffeeRecordEditComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
