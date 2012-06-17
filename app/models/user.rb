class User < ActiveRecord::Base
  # Include default devise modules. Others available are:
  # :token_authenticatable, :encryptable, :confirmable, :lockable, :timeoutable and :omniauthable
  devise :database_authenticatable,
         :recoverable, :rememberable, :trackable, :registerable

  # Setup accessible (or protected) attributes for your model
  attr_accessible :email, :password, :password_confirmation, :remember_me

  validates_uniqueness_of :email

  has_many :meeting_votes

  def vote_on meeting
    unless meeting_votes.where("meeting_id = ?", meeting.id).length > 0
      meeting_votes.create({ meeting_id: meeting.id })
    end
  end
end
