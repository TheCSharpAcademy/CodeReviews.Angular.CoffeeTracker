import { ComponentFixture, TestBed } from '@angular/core/testing';

import { CoffeeRecordsComponent } from './coffee-records.component';

describe('CoffeeRecordComponent', () => {
  let component: CoffeeRecordsComponent;
  let fixture: ComponentFixture<CoffeeRecordsComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      declarations: [CoffeeRecordsComponent]
    })
    .compileComponents();
    
    fixture = TestBed.createComponent(CoffeeRecordsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
