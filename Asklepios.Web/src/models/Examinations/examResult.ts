export interface ExamResultDto {
  examResultId: string;
  patientId: string;
  date: Date;
  result: string;
  comment: string;
  examId: number;
 }

 export interface ExamResultListDto {
  examResultId: string;
  patientId: string;
  date: Date;
  result: string;
  comment: string;
  examName: string;
 }

 export class InputCreateExamResult {
  examResultId:string = '';
  patientId:string = '';
  date: Date = new Date();
  result:string = '';
  comment:string = '';
  examId: number | null = null;
 }
