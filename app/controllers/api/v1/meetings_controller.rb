class Api::V1::MeetingsController < ApplicationController
  respond_to :json

  before_filter :authenticate_user!, only: [:create]
  after_filter :set_access_control_headers

  def index
    @meetings = Meeting.filter(params)
    
    respond_with meetings_json(@meetings)
  end

  def create

    @meeting = Meeting.new(params[:meeting])
    @meeting.user = @current_user

    if @meeting.save
      render json: @meeting, status: :created, location: @meeting
    else
      render json: @meeting.errors, status: :unprocessable_entity
    end

  end

  private

  def set_access_control_headers 
    headers['Access-Control-Allow-Origin'] = '*' 
    headers['Access-Control-Request-Method'] = '*' 
  end

  def meetings_json(meetings)
    meetings.collect do |meeting|
      meeting_json(meeting)
    end
  end

  def meeting_json(meeting)
    {
      id: meeting.id,
      title: meeting.title,
      starts_at: meeting.starts_at,
      description: meeting.description,
      location: meeting.location,
      url: meeting.url,
      organizer: meeting.organizer,
      costs_money: meeting.costs_money
    }
  end
end