class User < ActiveRecord::Base
  # Include default devise modules. Others available are:
  # :token_authenticatable, :encryptable, :confirmable, :lockable, :timeoutable and :omniauthable
  devise :database_authenticatable,
         :rememberable, :trackable, :registerable

  # Setup accessible (or protected) attributes for your model
  attr_accessible :email, :password, :password_confirmation, :remember_me

  validates_uniqueness_of :email

  has_many :meeting_votes

  def vote_on meeting
    unless meeting_votes.where("meeting_id = ?", meeting.id).length > 0
      meeting_votes.create({ meeting_id: meeting.id })
    end
  end
  
  def can_vote_on meeting
    meeting.meeting_votes.where("user_id = ?", self.id).empty?
  end

  def self.authenticate email, password
    if email.blank? || password.blank?
      false
    end
    
    user = User.find_for_database_authentication(:email=>email)
    
    if user.nil?
      false
    end

    if user.valid_password?(password)
      user
    else
      false
    end
  end
end
